using SaleMicroservice.API.Endpoints.Sales.Domain;

namespace SaleMicroservice.UnitTests.Domain.Entity;

public class SaleTestFixture : IDisposable
{
    public Sale GetValidSale()
        => new(GetValidItems());

    public List<SaleItem> GetValidItems()
        => new() { GetValidSaleItem() };

    public SaleItem GetValidSaleItem()
        => new(
            GetValidProductId(),
            GetValidQuantity(),
            GetValidUnitPrice(),
            GetValidWarranty()
        );

    public Guid GetValidProductId()
        => Guid.NewGuid();

    public int GetValidQuantity()
        => 1;

    public decimal GetValidUnitPrice()
        => 100.00m;

    public Warranty GetValidWarranty()
        => new(50.00m);

    public void Dispose()
    {
    }
}

[CollectionDefinition(nameof(SaleTestFixture))]
public class SaleTestFixtureCollection : ICollectionFixture<SaleTestFixture>
{
}