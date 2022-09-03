namespace Api.Commands.ItemCommands;

public class DeleteItemCommand : IRequest<Item>
{
    public int Id { get; set; }
}

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Item>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    public DeleteItemCommandHandler(IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Item> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
    {
        var existingRec = await _context.Items.SingleOrDefaultAsync(x => x.Id == command.Id);
        if (existingRec == null) throw new Exception("Recored not exist");
        _context.Items.Remove(existingRec);
        await _context.SaveChangesAsync();
        await _sender.SendMessagesAsync(existingRec);
        return existingRec;
    }
}