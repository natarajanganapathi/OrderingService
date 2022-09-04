namespace Api.Commands.OrderCommands;

public class UpdateAccountCommand : IRequest<Account>
{
    public int Id {get;set;}
    public String? AccountName { get; set; }
}

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Account>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    private ILogger<UpdateAccountCommandHandler> _logger;
    public UpdateAccountCommandHandler(ILogger<UpdateAccountCommandHandler> logger, IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        _logger = logger;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Account> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
         var existingRec = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == command.Id);
        var res = existingRec ?? throw new Exception("Recored not exist");
        var isDomainEventRequired = !string.IsNullOrWhiteSpace(res.AccountName) && res.AccountName.Equals(command.AccountName);
        existingRec.AccountName = command.AccountName;
        await _context.SaveChangesAsync();
        return existingRec;
    }
}