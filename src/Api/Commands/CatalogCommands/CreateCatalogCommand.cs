namespace Api.Commands.ItemCommands;

public class CreateCatalogCommand : IRequest<Catalog>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Stock { get; set; }
}

public class CreateCatalogCommandHandler : IRequestHandler<CreateCatalogCommand, Catalog>
{
    private readonly OrderDbContext _context;
 
    public CreateCatalogCommandHandler(IServiceScopeFactory scopeFactory)
    {
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Catalog> Handle(CreateCatalogCommand command, CancellationToken cancellationToken)
    {
        var item = new Catalog()
        {
            Name = command.Name,
            UnitPrice = command.UnitPrice,
            Discount = command.Discount,
            Stock = command.Stock
        };
        _context.Catalogs.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }
}