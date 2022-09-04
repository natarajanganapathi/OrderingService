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

    [HttpGet]
    [Route("{id:int}")]
    public async Task<Account> GetById(int id)
    {
        _logger.LogInformation($"Getting Account by id ${id}");
        return await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
    }

    [HttpPost]
    public void Post(CreateAccountCommand command)
    {
        _mediator.Send(command);
        _logger.LogInformation("Created new Account");
    }

    [HttpPost]
    [Route("{id:int}")]
    public void Update([FromBody] UpdateAccountCommand command, int id)
    {
        command.Id = id;
        _mediator.Send(command);
        _logger.LogInformation("Update Account");
    }

    [HttpDelete]
    [Route("{id:int}")]
    public void Delete(int id)
    {
        var command = new DeleteAccountCommand() { Id = id };
        _mediator.Send(command);
        _logger.LogInformation("Delete Account");
    }
}
