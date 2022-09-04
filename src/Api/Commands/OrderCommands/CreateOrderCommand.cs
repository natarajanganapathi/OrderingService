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
        await _context.SaveChangesAsync();

        var data = await _context.Catalogs
                        .Where(x => x.Id == orderEntry.Entity.CatalogId)
                        .GroupJoin(_context.Orders, a => a.Id, b => b.CatalogId, (a, b) => new { a = a, b = b })
                        .SelectMany(
                            temp => temp.b.DefaultIfEmpty(),
                            (temp, p) =>
                            new OrderSummaryData()
                            {
                                Name = temp.a.Name,
                                CatalogId = temp.a.Id,
                                Total = temp.b.Sum(x => x.Quantity)
                            })
                        .FirstOrDefaultAsync();
        if (data != null)
        {
            await _sender.SendMessagesAsync("add-summary-data", data);
        }
        return order;
    }
}
