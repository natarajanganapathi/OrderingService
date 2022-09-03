namespace Api.Commands.OrderCommands;

public class CreateOrderCommand : IRequest<Order>
{
    public String? Account { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly OrderDbContext _context;
    private MessageSender _sender;
    private ILogger<CreateOrderCommandHandler> _logger;
    public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger, IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        // _context = context;
        _sender = sender;
        _logger = logger;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order() { Account = command.Account ?? "" };
        try
        {
            _context.Orders.Add(order);
            var count = await _context.SaveChangesAsync();
            _logger.LogInformation($"Create Order Saved in Database. Count = {count}");
            await _sender.SendMessagesAsync(order);
            _logger.LogInformation($"Create Order Domain Event raised");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.ToString()}");
        }
        return order;
    }
}