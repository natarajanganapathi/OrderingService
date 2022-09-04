namespace BackgroundTasks.Tasks;

public class AddSummaryDataTask : BackgroundService
{
    private readonly ILogger<AddSummaryDataTask> _logger;
    private readonly MessageReceiver _receiver;
    private readonly SummaryRepository _repository;
    public AddSummaryDataTask(ILogger<AddSummaryDataTask> logger, MessageReceiver receiver, SummaryRepository repository)
    {
        _logger = logger;
        _receiver = receiver;
        _repository = repository;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Add Order Summary Task is starting.");
        await _receiver.ReceiveMessagesAsync("add-summary-data", "background-process", async (ProcessMessageEventArgs args) =>
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: {body}");
            var data = JsonSerializer.Deserialize<OrderSummaryData>(body);
            if (data != null)
            {
                await _repository.CreateAsync(data);
                _logger.LogDebug($"Added Summary Data created in database. Id={data.CatalogId}");
            }
            await args.CompleteMessageAsync(args.Message);
        });
        _logger.LogDebug("Add Order Summary Task is completed.");
    }
}