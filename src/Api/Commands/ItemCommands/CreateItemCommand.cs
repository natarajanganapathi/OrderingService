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
    private OrderDbContext _context;
    public CreateItemCommandHandler(OrderDbContext context)
    {
        _context = context;
    }
    public async Task<Item> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        var item = new Item() { 
            Id = command.Id,
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