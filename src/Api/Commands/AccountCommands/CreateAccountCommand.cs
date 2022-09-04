namespace Api.Commands.OrderCommands;

public class CreateAccountCommand : IRequest<Account>
{
    public String? AccountName { get; set; }
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    private ILogger<CreateAccountCommandHandler> _logger;
    public CreateAccountCommandHandler(ILogger<CreateAccountCommandHandler> logger, IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        _logger = logger;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Account> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var order = new Account() { AccountName = command.AccountName ?? "" };
        try
        {
            _context.Accounts.Add(order);
            var count = await _context.SaveChangesAsync();
            _logger.LogInformation($"Create Order Saved in Database. Count = {count}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.ToString()}");
        }
        return order;
    }
}