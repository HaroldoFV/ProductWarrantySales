using Bogus;
using ProductMicroservice.Domain.Entity;

namespace ProductMicroservice.UnitTests.Domain.Entity;

public class ProductTestFixture
{
    public Faker Faker { get; } = new Faker();

    public Product GetValidProduct()
    {
        var name = Faker.Commerce.ProductName();
        if (name.Length < 3) name = name.PadRight(3, 'a');
        if (name.Length > 255) name = name[..255];
        var supplier = Faker.Company.CompanyName();
        if (supplier.Length < 2) supplier = supplier.PadRight(2, 'a');
        if (supplier.Length > 100) supplier = supplier[..100];

        return new Product(
            name,
            Faker.Random.Decimal(1, 1000),
            0,
            100,
            10,
            supplier,
            Faker.Random.Bool()
        );
    }
}

[CollectionDefinition(nameof(ProductTestFixture))]
public class ProductTestFixtureCollection : ICollectionFixture<ProductTestFixture>
{ }