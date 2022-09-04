namespace Api.Commands.ItemCommands;

public class DeleteAccountCommand : IRequest<Account>
{
    public int Id { get; set; }
}

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Account>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    public DeleteAccountCommandHandler(IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Account> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
    {
        var existingRec = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == command.Id);
        if (existingRec == null) throw new Exception("Recored not exist");
        _context.Accounts.Remove(existingRec);
        await _context.SaveChangesAsync();
        // await _sender.SendMessagesAsync(existingRec);
        return existingRec;
    }
}