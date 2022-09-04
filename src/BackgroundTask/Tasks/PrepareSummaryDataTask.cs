namespace BackgroundTasks.Tasks;

public class PrepareSummaryDataTask : BackgroundService
{
    ILogger<PrepareSummaryDataTask> _logger;
    MessageReceiver _receiver;
    public PrepareSummaryDataTask(ILogger<PrepareSummaryDataTask> logger, MessageReceiver receiver)
    {
        _logger = logger;
        _receiver = receiver;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Prepare Summary Data Task is starting.");
        await _receiver.ReceiveMessagesAsync("parepare-summary-data", async (ProcessMessageEventArgs args) =>
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: {body}");
            await Task.FromResult("Hold");
        });
        _logger.LogDebug("Prepare Summary Data Task is completed.");
    }
}