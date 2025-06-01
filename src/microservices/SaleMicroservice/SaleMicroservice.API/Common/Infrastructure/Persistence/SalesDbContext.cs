using Microsoft.EntityFrameworkCore;
using SaleMicroservice.API.Endpoints.Sales.Domain;
using SalesMicroservice.API.Endpoints.Sales.Create.Data;

namespace SaleMicroservice.API.Common.Infrastructure.Persistence;

public class SalesDbContext : DbContext
{
    public DbSet<Sale> Sales => Set<Sale>();

    public SalesDbContext(DbContextOptions<SalesDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SaleConfiguration());
    }
}