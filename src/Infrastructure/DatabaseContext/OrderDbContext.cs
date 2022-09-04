namespace Infrastructure.DatabaseContext;

// #nullable disable warnings

public partial class OrderDbContext : DbContext
{
    public OrderDbContext() { }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Catalog> Catalogs { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity
                .Property<int>(nameof(Account.Id))
                .ValueGeneratedOnAdd();
            entity.ToTable("Account");
        });

        modelBuilder.Entity<Catalog>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity
                .Property<int>(nameof(Catalog.Id))
                .ValueGeneratedOnAdd();
            entity.ToTable("Catalog");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity
                .Property<int>(nameof(Order.Id))
                .ValueGeneratedOnAdd();
            entity.ToTable("Order");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}