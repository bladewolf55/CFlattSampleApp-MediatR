namespace CFlattSampleApp.Data;

public class CFlattSampleAppDbContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<State> States { get; set; } = null!;

    public CFlattSampleAppDbContext() { }

    public CFlattSampleAppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed States
        modelBuilder.Entity<State>().HasData(
            new State { Id = "CA", Name = "California" },
            new State { Id = "OR", Name = "Oregon" }
        );

        base.OnModelCreating(modelBuilder);
    }
}