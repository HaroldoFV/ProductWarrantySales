using FluentAssertions;
using ProductMicroservice.Domain.Entity;
using ProductMicroservice.Domain.Exceptions;

namespace ProductMicroservice.UnitTests.Domain.Entity;

[Collection(nameof(ProductTestFixture))]
public class ProductTest
{
    private readonly ProductTestFixture _fixture;

    public ProductTest(ProductTestFixture fixture) => _fixture = fixture;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Product - Aggregates")]
    public void Instantiate()
    {
        var valid = _fixture.GetValidProduct();

        var product = new Product(
            valid.Name, valid.Price, valid.MinimumStock, valid.MaximumStock, valid.Stock, valid.Supplier,
            valid.HasWarranty);

        product.Should().NotBeNull();
        product.Name.Should().Be(valid.Name);
        product.Price.Should().Be(valid.Price);
        product.MinimumStock.Should().Be(valid.MinimumStock);
        product.MaximumStock.Should().Be(valid.MaximumStock);
        product.Stock.Should().Be(valid.Stock);
        product.Supplier.Should().Be(valid.Supplier);
        product.HasWarranty.Should().Be(valid.HasWarranty);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsInvalid))]
    [Trait("Domain", "Product - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void InstantiateErrorWhenNameIsInvalid(string? name)
    {
        var valid = _fixture.GetValidProduct();
        Action act = () => new Product(
            name!, valid.Price, valid.MinimumStock, valid.MaximumStock, valid.Stock, valid.Supplier, valid.HasWarranty);

        act.Should().Throw<EntityValidationException>();
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenPriceIsInvalid))]
    [Trait("Domain", "Product - Aggregates")]
    [InlineData(-1)]
    [InlineData(-100)]
    public void InstantiateErrorWhenPriceIsInvalid(decimal price)
    {
        var valid = _fixture.GetValidProduct();
        Action act = () => new Product(
            valid.Name, price, valid.MinimumStock, valid.MaximumStock, valid.Stock, valid.Supplier, valid.HasWarranty);

        act.Should().Throw<EntityValidationException>();
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenStockOutOfRange))]
    [Trait("Domain", "Product - Aggregates")]
    public void InstantiateErrorWhenStockOutOfRange()
    {
        var valid = _fixture.GetValidProduct();
        Action act = () => new Product(
            valid.Name, valid.Price, 0, 10, 11, valid.Supplier, valid.HasWarranty);

        act.Should().Throw<EntityValidationException>();
    }

    [Fact(DisplayName = nameof(UpdateStockIncreases))]
    [Trait("Domain", "Product - Aggregates")]
    public void UpdateStockIncreases()
    {
        var valid = _fixture.GetValidProduct();
        var product = new Product(
            valid.Name, valid.Price, valid.MinimumStock, valid.MaximumStock, valid.Stock, valid.Supplier,
            valid.HasWarranty);

        product.UpdateStock(2);

        product.Stock.Should().Be(valid.Stock + 2);
    }

    [Fact(DisplayName = nameof(UpdateStockDecreases))]
    [Trait("Domain", "Product - Aggregates")]
    public void UpdateStockDecreases()
    {
        var valid = _fixture.GetValidProduct();
        var product = new Product(
            valid.Name, valid.Price, valid.MinimumStock, valid.MaximumStock, valid.Stock, valid.Supplier,
            valid.HasWarranty);

        product.UpdateStock(-1);

        product.Stock.Should().Be(valid.Stock - 1);
    }

    [Fact(DisplayName = nameof(UpdateStockThrowsWhenInsufficient))]
    [Trait("Domain", "Product - Aggregates")]
    public void UpdateStockThrowsWhenInsufficient()
    {
        var valid = _fixture.GetValidProduct();
        var product = new Product(
            valid.Name, valid.Price, valid.MinimumStock, valid.MaximumStock, valid.Stock, valid.Supplier,
            valid.HasWarranty);

        Action act = () => product.UpdateStock(-(valid.Stock + 1));

        act.Should().Throw<InvalidOperationException>();
    }
}