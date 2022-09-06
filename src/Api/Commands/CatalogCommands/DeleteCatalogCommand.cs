namespace Api.Commands.ItemCommands;

public class DeleteCatalogCommand : IRequest<Catalog>
{
    public int Id { get; set; }
}

public class DeleteCatalogCommandHandler : IRequestHandler<DeleteCatalogCommand, Catalog>
{
    private readonly OrderDbContext _context;
    private readonly MessageSender _sender;
    public DeleteCatalogCommandHandler(IServiceScopeFactory scopeFactory, MessageSender sender)
    {
        _sender = sender;
        var serviceScope = scopeFactory.CreateScope();
        _context = serviceScope.ServiceProvider.GetService<OrderDbContext>() ?? throw new Exception("Order Database context should not be null");
    }
    public async Task<Catalog> Handle(DeleteCatalogCommand command, CancellationToken cancellationToken)
    {
        var existingRec = await _context.Catalogs.SingleOrDefaultAsync(x => x.Id == command.Id);
        if (existingRec == null) throw new Exception("Recored not exist");
        _context.Catalogs.Remove(existingRec);
        await _context.SaveChangesAsync();
        return existingRec;
    }
}