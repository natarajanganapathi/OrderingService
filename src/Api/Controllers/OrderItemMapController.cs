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
    public async Task<List<OrderItemMap>> Get()
    {
        return await _context.OrderItemMaps.Take(100).ToListAsync();
    }

    [HttpPost]
    public void Post(CreateOrderItemMapCommand command)
    {
        _mediator.Send(command);
    }
}
