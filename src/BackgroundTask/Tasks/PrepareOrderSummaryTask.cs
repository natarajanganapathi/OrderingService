namespace BackgroundTasks.Tasks;

public class PrepareOrderSummaryTask : BackgroundService
{
    ILogger<InitialLoadMongoDbTask> _logger;
    MessageReceiver _receiver;
    public PrepareOrderSummaryTask(ILogger<InitialLoadMongoDbTask> logger, MessageReceiver receiver)
    {
        _logger = logger;
        _receiver = receiver;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("UpdateMongoDbTask is starting.");
        await _receiver.ReceiveMessagesAsync("UpdateMongoDb", async (ProcessMessageEventArgs args) =>
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: {body}");
        });
        _logger.LogDebug("UpdateMongoDbTask is completed.");
    }
}