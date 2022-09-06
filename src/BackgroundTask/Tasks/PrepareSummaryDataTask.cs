using System.Linq;
using Domain.DomainModel.OrderDomainModel.Entity;

namespace BackgroundTasks.Tasks;

public class PrepareSummaryDataTask : BackgroundService
{
    private readonly ILogger<PrepareSummaryDataTask> _logger;
    private readonly MessageReceiver _receiver;
    private readonly SummaryRepository _repository;
    private readonly IServiceScopeFactory _scopeFactory;
    public PrepareSummaryDataTask(ILogger<PrepareSummaryDataTask> logger, MessageReceiver receiver, SummaryRepository repository, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _receiver = receiver;
        _repository = repository;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Add Order Summary Task is listening.");
        await _receiver.ReceiveMessagesAsync("prepare-summary-data", "background-process", async (ProcessMessageEventArgs args) =>
        {
            try
            {
                var serviceScope = _scopeFactory.CreateScope();
                var context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");

                string body = args.Message.Body.ToString();
                _logger.LogInformation($"Received: {body}");
                var order = JsonSerializer.Deserialize<Order>(body);
                if (order != null)
                {
                    var catalogs = context.Catalogs
                                    .Where(x => x.Id == order.CatalogId).ToList();
                    var data = catalogs.GroupJoin(context.Orders, a => a.Id, b => b.CatalogId, (a, b) => new { a = a, b = b })
                                    .SelectMany(
                                        temp => temp.b.DefaultIfEmpty(),
                                        (temp, p) =>
                                        new OrderSummaryData()
                                        {
                                            Name = temp.a.Name,
                                            CatalogId = temp.a.Id,
                                            Total = temp.b.Sum(x => x.Quantity)
                                        })
                                    .FirstOrDefault();
                    if (data != null)
                    {
                        if (data.Total == 0)
                        {
                            await _repository.DeleteAsync(data.CatalogId);
                        }
                        else
                        {
                            await _repository.UpdateAsync(data);
                        }
                        _logger.LogInformation($"Summary Data updated in database. Id={data.CatalogId} Name={data.Name} Total={data.Total}");
                    }
                }
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        });
    }
}