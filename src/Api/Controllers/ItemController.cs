namespace Api.Controller;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly IMediator _mediator;
    private readonly OrderDbContext _context;

    public ItemController(ILogger<ItemController> logger, IMediator mediator, OrderDbContext context)
    {
        _logger = logger;
        _mediator = mediator;
        _context = context;
    }

    [HttpGet]
    public async Task<List<Item>> Get()
    {
      return await _context.Items.Take(100).ToListAsync();
    }

    [HttpPost]
    public void Post(UpdateItemCommand command)
    {
        _mediator.Send(command);
    }
}
