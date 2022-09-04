namespace Api.Controller;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly IMediator _mediator;
    private readonly OrderDbContext _context;

    public CatalogController(ILogger<CatalogController> logger, IMediator mediator, OrderDbContext context)
    {
        _logger = logger;
        _mediator = mediator;
        _context = context;
    }

    [HttpGet]
    public async Task<List<Catalog>> Get()
    {
      return await _context.Items.Take(100).ToListAsync();
    }

    [HttpPost]
    public void Post(UpdateCatalogCommand command)
    {
        _mediator.Send(command);
    }
}
