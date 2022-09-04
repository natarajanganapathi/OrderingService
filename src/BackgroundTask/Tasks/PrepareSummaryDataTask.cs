namespace BackgroundTasks.Tasks;

public class PrepareSummaryDataTask : BackgroundService
{
    private readonly ILogger<PrepareSummaryDataTask> _logger;
    private readonly MessageReceiver _receiver;
    private readonly SummaryRepository _repository;
    public PrepareSummaryDataTask(ILogger<PrepareSummaryDataTask> logger, MessageReceiver receiver, SummaryRepository repository)
    {
        _logger = logger;
        _receiver = receiver;
        _repository = repository;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Prepare Summary Data Task is starting.");
        await _receiver.ReceiveMessagesAsync("prepare-summary-data", "background-process", async (ProcessMessageEventArgs args) =>
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: {body}");
            var data = JsonSerializer.Deserialize<List<OrderSummaryData>>(body);
            if (data != null)
            {
                await _repository.CreateManyAsync(data);
                _logger.LogDebug("Prepare Summary Data created in database");
            }
            await args.CompleteMessageAsync(args.Message);
        });
        _logger.LogDebug("Prepare Summary Data Task is completed.");
    }
}
