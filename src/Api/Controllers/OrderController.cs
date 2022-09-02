namespace Api.Controller;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IMediator _mediator;
    private readonly OrderDbContext _context;

    public OrderController(ILogger<OrderController> logger, IMediator mediator, OrderDbContext context)
    {
        _logger = logger;
        _mediator = mediator;
        _context = context;
    }

    [HttpGet]
    public async Task<List<Order>> Get()
    {
        _logger.LogInformation("Getting Orders list");
      return await _context.Orders.Take(100).ToListAsync();
    }

    [HttpPost]
    public void Post(CreateOrderCommand command)
    {
         _logger.LogInformation("Creating new Order");
        _mediator.Send(command);
    }
}
