namespace Api.Commands.OrderItemMapCommands;

public class OrderCommand : IRequest<OrderItemMap>
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderItemMapCommandHandler : IRequestHandler<OrderCommand, OrderItemMap>
{
    private readonly OrderDbContext _context;
    public CreateOrderItemMapCommandHandler(OrderDbContext context)
    {
        _context = context;
    }
    public async Task<OrderItemMap> Handle(OrderCommand command, CancellationToken cancellationToken)
    {
        var orderItemMap = new OrderItemMap() { 
            OrderId = command.OrderId,
            ItemId = command.ItemId,
            Quantity = command.Quantity
        };
        _context.OrderItemMaps.Add(orderItemMap);
        await _context.SaveChangesAsync();
        return orderItemMap;
    }
}
