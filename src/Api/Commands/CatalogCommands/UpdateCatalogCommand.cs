namespace Api.Commands.ItemCommands;

public class UpdateCatalogCommand : IRequest<Catalog>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Stock { get; set; }
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
        var rec = existingRec ?? throw new Exception("Recored not exist");
        var isDomainEventRequired = rec.Name != null && rec.Name.Equals(command.Name);
        existingRec.Name = command.Name;
        existingRec.UnitPrice = command.UnitPrice;
        existingRec.Discount = command.Discount;
        existingRec.Stock = command.Stock;
        await _context.SaveChangesAsync();
        if (isDomainEventRequired)
        {
            var data = await _context.Catalogs
                           .Where(x => x.Id == command.Id)
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
                await _sender.SendMessagesAsync("update-summary-data", data);
            }
        }
        return existingRec;
    }
}