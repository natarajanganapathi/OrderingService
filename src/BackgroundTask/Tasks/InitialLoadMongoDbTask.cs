namespace BackgroundTasks.Tasks;

public class InitialLoadMongoDbTask : BackgroundService
{
    ILogger<InitialLoadMongoDbTask> _logger;
    MessageReceiver _receiver;
    public InitialLoadMongoDbTask(ILogger<InitialLoadMongoDbTask> logger, MessageReceiver receiver)
    {
        _logger = logger;
        _receiver = receiver;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("InitialLoadMongoDbTask is starting.");
        await _receiver.ReceiveMessagesAsync("InitialLoadMongoDb", async (ProcessMessageEventArgs args) =>
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: {body}");
        });
        _logger.LogDebug("InitialLoadMongoDbTask is completed.");
    }
}