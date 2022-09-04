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
         _logger.LogInformation("Getting Order list");
        return await _context.Orders.Take(100).ToListAsync();
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<Order> GetById(int id)
    {
        _logger.LogInformation($"Getting Order by id ${id}");
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }

    [HttpPost]
    public void Post(CreateOrderCommand command)
    {
        _mediator.Send(command);
        _logger.LogInformation("Created new Order");
    }

    [HttpPost]
    [Route("{id:int}")]
    public void Update([FromBody] UpdateOrderCommand command, int id)
    {
        command.Id = id;
        _mediator.Send(command);
        _logger.LogInformation("Update Order");
    }

    [HttpDelete]
    [Route("{id:int}")]
    public void Delete(int id)
    {
        var command = new DeleteOrderCommand() { Id = id };
        _mediator.Send(command);
        _logger.LogInformation("Delete Order");
    }
}
