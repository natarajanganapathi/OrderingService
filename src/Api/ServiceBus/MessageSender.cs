namespace Api.ServiceBus;

public class MessageSender
{
    IConfiguration _configuration;
    public MessageSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMessagesAsync<T>(T message)
    {
        await using ServiceBusClient client = new ServiceBusClient(_configuration["ServiceBusConnectionString"]);
        ServiceBusSender sender = client.CreateSender(_configuration["TopicName"]);
        var messageString = JsonSerializer.Serialize(message);
        ServiceBusMessage serviceMessage = new ServiceBusMessage(messageString);
        await sender.SendMessageAsync(serviceMessage);
    }
}
