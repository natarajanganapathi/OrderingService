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
            var account = new Account { AccountName = "Saravana Store" };
            var catalog1 = new Catalog { Name = "Shirt", Discount = 0, UnitPrice = 10, Stock = 150 };
            var catalog2 = new Catalog { Name = "Pant", Discount = 0, UnitPrice = 60, Stock = 140 };

            if (!context.Orders.Any())
            {
                var acnt = context.Add(account);
                var catl1 = context.Add(catalog1);
                var catl2 = context.Add(catalog2);

                context.SaveChanges();

                context.Add(new Order() { AccountId = acnt.Entity.Id, CatalogId = catl1.Entity.Id, Quantity = 14 });
                context.Add(new Order() { AccountId = acnt.Entity.Id, CatalogId = catl2.Entity.Id, Quantity = 11 });
                context.SaveChanges();
            }
        }
    }
}
