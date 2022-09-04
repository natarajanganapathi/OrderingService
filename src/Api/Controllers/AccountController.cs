namespace Api.Controller;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IMediator _mediator;
    private readonly OrderDbContext _context;

    public AccountController(ILogger<AccountController> logger, IMediator mediator, OrderDbContext context)
    {
        _logger = logger;
        _mediator = mediator;
        _context = context;
    }

    [HttpGet]
    public async Task<List<Account>> Get()
    {
        _logger.LogInformation("Getting Account list");
        return await _context.Accounts.Take(100).ToListAsync();
    }

    [HttpPost]
    public void Post(CreateAccountCommand command)
    {
        _mediator.Send(command);
        _logger.LogInformation("Created new Account");
    }

    [HttpPatch]
    public void Patch(UpdateAccountCommand command)
    { 
        _mediator.Send(command); 
        _logger.LogInformation("Update Account");
    }

    [HttpDelete]
    public void Delete(DeleteAccountCommand command)
    {
        _mediator.Send(command);
        _logger.LogInformation("Delete Account");
    }
}
