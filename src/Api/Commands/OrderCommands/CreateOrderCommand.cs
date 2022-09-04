namespace Api.Commands.OrderItemMapCommands;

public class OrderCommand : IRequest<Order>
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderItemMapCommandHandler : IRequestHandler<OrderCommand, Order>
{
    private readonly OrderDbContext _context;
    public CreateOrderItemMapCommandHandler(OrderDbContext context)
    {
        _context = context;
    }
    public async Task<Order> Handle(OrderCommand command, CancellationToken cancellationToken)
    {
        var orderItemMap = new Order() { 
            OrderId = command.OrderId,
            ItemId = command.ItemId,
            Quantity = command.Quantity
        };
        _context.OrderItemMaps.Add(orderItemMap);
        await _context.SaveChangesAsync();
        return orderItemMap;
    }
}
