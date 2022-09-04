namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    // public IUnitOfWork UnitOfWork
    // {
    //     get
    //     {
    //         return _context;
    //     }
    // }

    public OrderRepository(OrderDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Account Add(Account order)
    {
        return _context.Accounts.Add(order).Entity;

    }

    public async Task<Account?> GetAsync(int orderId)
    {
        var order = await _context
                            .Accounts
                            // .Include(x => x.Address)
                            .FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null)
        {
            order = _context
                        .Accounts
                        .Local
                        .FirstOrDefault(o => o.Id == orderId);
        }
        // if (order != null)
        // {
            // await _context.Entry(order)
            //     .Collection(i => i.OrderItems).LoadAsync();
            // await _context.Entry(order)
            //     .Reference(i => i.OrderStatus).LoadAsync();
        // }

        return order;
    }

    public void Update(Account order)
    {
        _context.Entry(order).State = EntityState.Modified;
    }
}