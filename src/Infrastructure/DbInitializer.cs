namespace Infrastructure;

public interface IDbInitializer
{
    /// <summary>
    /// Applies any pending migrations for the context to the database.
    /// Will create the database if it does not already exist.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Adds some default values to the Db
    /// </summary>
    void SeedData();
}

public class DbInitializer : IDbInitializer
{
    private readonly IServiceScopeFactory _scopeFactory;
    public DbInitializer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        // _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));
    }

    public void Initialize()
    {
        var serviceScope = _scopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<OrderDbContext>();
        // context?.Database.EnsureDeleted();
        context?.Database.EnsureCreated();
        context?.Database.Migrate();
    }

    public void SeedData()
    {
        var serviceScope = _scopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<OrderDbContext>();
        if (context != null)
        {
            // Add default roles
            var order = new Account { AccountName = "Saravana Store" };
            var orderItem1 = new Catalog { Name = "Shirt", Discount = 0, UnitPrice = 10, Stock = 150 };
            var orderItem2 = new Catalog { Name = "Pant", Discount = 0, UnitPrice = 60, Stock = 140 };

            if (!context.Orders.Any())
            {
                var ordr = context.Add(order);
                var itm1 = context.Add(orderItem1);
                var itm2 = context.Add(orderItem2);

                context.SaveChanges();

                context.Add(new Order() { OrderId = ordr.Entity.Id, ItemId = itm1.Entity.Id, Quantity = 14 });
                context.Add(new Order() { OrderId = ordr.Entity.Id, ItemId = itm2.Entity.Id, Quantity = 11 });
                context.SaveChanges();
            }
        }
    }
}
