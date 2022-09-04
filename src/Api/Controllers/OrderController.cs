namespace Api.Controller;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IMediator _mediator;
    private readonly OrderDbContext _context;

    public OrdersController(ILogger<OrdersController> logger, IMediator mediator, OrderDbContext context)
    {
        _logger = logger;
        _mediator = mediator;
        _context = context;
    }

    [HttpGet]
    public async Task<List<Order>> Get()
    {
        return await _context.Orders.Take(100).ToListAsync();
    }

    [HttpPost]
    public void Post(OrderCommand command)
    {
        _mediator.Send(command);
    }
}
