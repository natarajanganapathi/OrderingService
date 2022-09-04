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
    public CreateOrderItemMapCommandHandler(OrderDbContext context)
    {
        _context = context;
    }
    public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderItemMap = new Order() { 
            AccountId = command.AccountId,
            CatalogId = command.CatalogId,
            Quantity = command.Quantity
        };
        _context.Orders.Add(orderItemMap);
        await _context.SaveChangesAsync();
        return orderItemMap;
    }
}
