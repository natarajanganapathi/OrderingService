namespace Api.Commands.ItemCommands;

public class CreateItemCommand : IRequest<Item>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }
}

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Item>
{
    private readonly OrderDbContext _context;
 
    public CreateItemCommandHandler(IServiceScopeFactory scopeFactory)
    {
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Item> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        var item = new Item()
        {
            Name = command.Name,
            UnitPrice = command.UnitPrice,
            Discount = command.Discount,
            Units = command.Units
        };
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }
}