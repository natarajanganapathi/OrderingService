namespace BackgroundTasks.Tasks;

public class UpdateSummaryDataTask : BackgroundService
{
    private readonly ILogger<UpdateSummaryDataTask> _logger;
    private readonly MessageReceiver _receiver;
    private readonly SummaryRepository _repository;
    public UpdateSummaryDataTask(ILogger<UpdateSummaryDataTask> logger, MessageReceiver receiver, SummaryRepository repository)
    {
        _logger = logger;
        _receiver = receiver;
        _repository = repository;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Update Order Summary Task is starting.");
        await _receiver.ReceiveMessagesAsync("update-summary-data", "background-process", async (ProcessMessageEventArgs args) =>
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: {body}");
            var data = JsonSerializer.Deserialize<OrderSummaryData>(body);
            if (data != null)
            {
                await _repository.UpdateAsync(data);
                _logger.LogDebug($"Updated Summary Data in database. Id = {data.ItemId}");
            }
            await args.CompleteMessageAsync(args.Message);
        });
        _logger.LogDebug("Update Order Summary Task is completed.");
    }
}