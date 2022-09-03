namespace Api.Commands.OrderItemMapCommands;

public class CreateOrderItemMapCommand : IRequest<OrderItemMap>
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderItemMapCommandHandler : IRequestHandler<CreateOrderItemMapCommand, OrderItemMap>
{
    private readonly OrderDbContext _context;
    public CreateOrderItemMapCommandHandler(OrderDbContext context)
    {
        _context = context;
    }
    public async Task<OrderItemMap> Handle(CreateOrderItemMapCommand command, CancellationToken cancellationToken)
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
