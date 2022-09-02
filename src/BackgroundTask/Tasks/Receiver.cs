namespace BackgroundTasks.Tasks;

public class MessageReceiver
{
    IConfiguration _configuration;
    public MessageReceiver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // static async Task MessageHandler(ProcessMessageEventArgs args)
    // {
    //     string body = args.Message.Body.ToString();
    //     Console.WriteLine($"Message received: {body}");

    //     await args.CompleteMessageAsync(args.Message);
    // }

    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }

    public async Task ReceiveMessagesAsync(String subscriberName, Func<ProcessMessageEventArgs, Task> handler)
    {
        await using ServiceBusClient client = new ServiceBusClient(_configuration["ServiceBusConnectionString"]);
        // create a processor that we can use to process the messages
        ServiceBusProcessor processor = client.CreateProcessor(_configuration["TopicName"], subscriberName, new ServiceBusProcessorOptions());
        try
        {
            // add handler to process messages
            processor.ProcessMessageAsync += handler;
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();

            // Console.WriteLine("Wait for a minute and then press any key to end the processing");
            // Console.ReadKey();

            // stop processing 
            // await processor.StopProcessingAsync();
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            // await processor.DisposeAsync();
            // await client.DisposeAsync();
        }
    }
}