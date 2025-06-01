using WarrantyMicroservice.Domain.Entity;

namespace WarrantyMicroservice.UnitTests.Domain.Entity;

public class WarrantyTestFixture : IDisposable
{
    public Warranty GetValidWarranty()
        => new(
            GetValidName(),
            GetValidValue(),
            GetValidTermInYears()
        );

    public static string GetValidName() => "Extended Warranty";
    public static decimal GetValidValue() => 299.99m;
    public static int GetValidTermInYears() => 2;

    public void Dispose()
    {
    }
}

[CollectionDefinition(nameof(WarrantyTestFixture))]
public class WarrantyTestFixtureCollection : ICollectionFixture<WarrantyTestFixture>
{
}