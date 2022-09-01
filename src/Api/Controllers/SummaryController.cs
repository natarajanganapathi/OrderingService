namespace Api.Controller;

[ApiController]
[Route("[controller]")]
public class SummaryController : ControllerBase
{
    private readonly ILogger<SummaryController> _logger;
    private readonly SummaryDataContext _context;
    private readonly SummaryRepository _repository;

    public SummaryController(ILogger<SummaryController> logger, SummaryRepository repository, SummaryDataContext context)
    {
        _logger = logger;
        _context = context;
        _repository = repository;
    }

    [HttpGet]
    public List<OrderSummaryData> Get()
    {
        return _repository.GetAsync();
    }
}
