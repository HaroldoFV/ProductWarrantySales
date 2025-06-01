using Microsoft.EntityFrameworkCore;
using WarrantyMicroservice.Domain.Entity;

namespace WarrantyMicroservice.Infra.Data.EF;

public class WarrantyDbContext
    : DbContext
{
    public DbSet<Warranty> Warranties => Set<Warranty>();

    public WarrantyDbContext(
        DbContextOptions<WarrantyDbContext> options
    ) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.ApplyConfiguration(new WarrantyConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entries = ChangeTracker
            .Entries<Warranty>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Property("LastUpdated").CurrentValue = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}