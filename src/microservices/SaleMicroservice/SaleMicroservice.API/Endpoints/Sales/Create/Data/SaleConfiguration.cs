using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleMicroservice.API.Endpoints.Sales.Domain;

namespace SalesMicroservice.API.Endpoints.Sales.Create.Data;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TotalAmount)
            .HasPrecision(18, 2);

        builder.OwnsMany(x => x.Items, navigation =>
        {
            navigation.WithOwner().HasForeignKey("SaleId");
            navigation.Property(x => x.UnitPrice).HasPrecision(18, 2);
            navigation.Property(x => x.TotalAmount).HasPrecision(18, 2);
            navigation.OwnsOne(x => x.Warranty);
        });
    }
}