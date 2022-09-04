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
        _logger.LogInformation("Getting Orders list");
        return await _context.Orders.Take(100).ToListAsync();
    }

    [HttpPost]
    public void Post(CreateAccountCommand command)
    {
        try
        {
            _logger.LogInformation("Creating new Order");
            _mediator.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
    }
}
