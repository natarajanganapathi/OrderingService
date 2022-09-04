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
        return await _context.Catalogs.Take(100).ToListAsync();
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<Catalog> GetById(int id)
    {
        _logger.LogInformation($"Getting Catalogs by id ${id}");
        return await _context.Catalogs.FirstOrDefaultAsync(x => x.Id == id);
    }

    [HttpPost]
    public void Post(CreateCatalogCommand command)
    {
        _mediator.Send(command);
        _logger.LogInformation("Created new Catalog");
    }

    [HttpPost]
    [Route("{id:int}")]
    public void Update([FromBody] UpdateCatalogCommand command, int id)
    {
        command.Id = id;
        _mediator.Send(command);
        _logger.LogInformation("Update Catalog");
    }

    [HttpDelete]
    [Route("{id:int}")]
    public void Delete(int id)
    {
        var command = new DeleteCatalogCommand() { Id = id };
        _mediator.Send(command);
        _logger.LogInformation("Delete Catalog");
    }
}
