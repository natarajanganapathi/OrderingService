namespace Infrastructure;

public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>()
            .UseSqlServer(args[0]);
        return new OrderDbContext(optionsBuilder.Options);
    }
}
