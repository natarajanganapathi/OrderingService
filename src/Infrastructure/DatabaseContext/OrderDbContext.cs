namespace Infrastructure.DatabaseContext;

// #nullable disable warnings

public partial class OrderDbContext : DbContext
{
    public OrderDbContext() { }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public virtual DbSet<Account> Orders { get; set; }
    public virtual DbSet<Catalog> Items { get; set; }
    public virtual DbSet<OrderItemMap> OrderItemMaps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity
                .Property<int>(nameof(Account.Id))
                .ValueGeneratedOnAdd();
            entity.ToTable("Order");
        });

        modelBuilder.Entity<Catalog>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity
                .Property<int>(nameof(Catalog.Id))
                .ValueGeneratedOnAdd();
            entity.ToTable("Item");
        });

        modelBuilder.Entity<OrderItemMap>(entity =>
        {
            entity.HasKey(nameof(OrderItemMap.OrderId), nameof(OrderItemMap.ItemId));
            entity.ToTable("OrderItemMap");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}