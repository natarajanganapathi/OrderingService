namespace Api.Commands.ItemCommands;

public class UpdateCatalogCommand : IRequest<Catalog>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }
}

public class UpdateCatalogCommandHandler : IRequestHandler<UpdateCatalogCommand, Catalog>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    public UpdateCatalogCommandHandler(IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Catalog> Handle(UpdateCatalogCommand command, CancellationToken cancellationToken)
    {
        var existingRec = await _context.Catalogs.SingleOrDefaultAsync(x => x.Id == command.Id);
        var res = existingRec ?? throw new Exception("Recored not exist");
        var isDomainEventRequired = res.Name != null && res.Name.Equals(command.Name);
        existingRec.Name = command.Name;
        existingRec.UnitPrice = command.UnitPrice;
        existingRec.Discount = command.Discount;
        existingRec.Units = command.Units;
        await _context.SaveChangesAsync();
        if (isDomainEventRequired)
        {
            await _sender.SendMessagesAsync(res);
        }
        return existingRec;
    }
}