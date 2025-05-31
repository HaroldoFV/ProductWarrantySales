using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Domain.Entity;
using ProductMicroservice.Infra.Data.EF.Configurations;

namespace ProductMicroservice.Infra.Data.EF;

public class ProductWarrantySalesDbContext
    : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public ProductWarrantySalesDbContext(
        DbContextOptions<ProductWarrantySalesDbContext> options
    ) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProductConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entries = ChangeTracker
            .Entries<Product>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Property("LastUpdated").CurrentValue = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}