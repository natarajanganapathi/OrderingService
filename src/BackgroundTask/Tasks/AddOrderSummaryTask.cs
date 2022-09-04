namespace BackgroundTasks.Tasks;

public class AddSummaryDataTask : BackgroundService
{
    ILogger<PrepareSummaryDataTask> _logger;
    MessageReceiver _receiver;
    public AddSummaryDataTask(ILogger<PrepareSummaryDataTask> logger, MessageReceiver receiver)
    {
        _logger = logger;
        _receiver = receiver;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Prepare Order Summary Task is starting.");
        await _receiver.ReceiveMessagesAsync("add-summary-data", async (ProcessMessageEventArgs args) =>
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: {body}");
            // await args.CompleteMessageAsync(args.Message);
            await Task.FromResult("Hold");
        });
        _logger.LogDebug("Prepare Order Summary Task is completed.");
    }
}