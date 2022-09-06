namespace Api.Commands.OrderItemMapCommands;

public class CreateOrderCommand : IRequest<Order>
{
    public int AccountId { get; set; }
    public int CatalogId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderItemMapCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    public CreateOrderItemMapCommandHandler(IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
        _sender = sender;
    }
    public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order()
        {
            AccountId = command.AccountId,
            CatalogId = command.CatalogId,
            Quantity = command.Quantity
        };
        var orderEntry = _context.Orders.Add(order);
        // Regular update to corresponding SQL Table containing orders
        await _context.SaveChangesAsync();
        // Raise Domain Event 
        await _sender.SendMessagesAsync("prepare-summary-data", orderEntry.Entity);
        return orderEntry.Entity;
    }
}
