namespace Api.Commands.ItemCommands;

public class UpdateItemCommand : IRequest<Item>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Item>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    public UpdateItemCommandHandler(IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Item> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
    {
        var existingRec = await _context.Items.SingleOrDefaultAsync(x => x.Id == command.Id);
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