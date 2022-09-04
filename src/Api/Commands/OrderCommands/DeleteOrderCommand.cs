namespace Api.Commands.ItemCommands;

public class DeleteOrderCommand : IRequest<Order>
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int CatalogId { get; set; }
    public int Quantity { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Order>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    public DeleteOrderCommandHandler(IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Order> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var existingRec = await _context.Orders.SingleOrDefaultAsync(x => x.Id == command.Id);
        if (existingRec == null) throw new Exception("Recored not exist");
        _context.Orders.Remove(existingRec);
        await _context.SaveChangesAsync();
        // await _sender.SendMessagesAsync(existingRec);
        return existingRec;
    }
}