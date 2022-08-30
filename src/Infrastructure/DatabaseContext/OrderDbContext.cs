namespace Infrastructure.DatabaseContext;

// #nullable disable warnings

public partial class OrderDbContext : DbContext
{
    public OrderDbContext() { }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<OrderItemMap> OrderItemMaps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity
                .Property<int>(nameof(Order.Id))
                .ValueGeneratedOnAdd();
            entity.ToTable("Order");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity
                .Property<int>(nameof(Item.Id))
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