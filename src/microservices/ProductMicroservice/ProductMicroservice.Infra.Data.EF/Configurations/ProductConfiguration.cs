using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductMicroservice.Domain.Entity;

namespace ProductMicroservice.Infra.Data.EF.Configurations;

internal class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.MinimumStock)
            .IsRequired();

        builder.Property(p => p.MaximumStock)
            .IsRequired();

        builder.Property(p => p.Stock)
            .IsRequired();

        builder.Property(p => p.Supplier)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.HasWarranty)
            .IsRequired();

        builder.Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}