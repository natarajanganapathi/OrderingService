namespace Api.Commands.ItemCommands;

public class UpdateOrderCommand : IRequest<Order>
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int CatalogId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    public UpdateOrderCommandHandler(IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Order> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var existingRec = await _context.Orders.SingleOrDefaultAsync(x => x.Id == command.Id);
        var rec = existingRec ?? throw new Exception("Recored not exist");
        var isDomainEventRequired = (rec.CatalogId != command.CatalogId) || (rec.Quantity != command.Quantity);

        existingRec.AccountId = command.AccountId;
        existingRec.CatalogId = command.CatalogId;
        existingRec.Quantity = command.Quantity;

        await _context.SaveChangesAsync();
        if (isDomainEventRequired)
        {
            await _sender.SendMessagesAsync("prepare-summary-data", existingRec);
        }
        return existingRec;
    }
}