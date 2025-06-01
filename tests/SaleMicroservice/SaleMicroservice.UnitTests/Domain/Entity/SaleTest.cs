using FluentAssertions;
using SaleMicroservice.API.Common.Exceptions;
using SaleMicroservice.API.Endpoints.Sales.Domain;

namespace SaleMicroservice.UnitTests.Domain.Entity;

[Collection(nameof(SaleTestFixture))]
public class SaleTest
{
    private readonly SaleTestFixture _fixture;

    public SaleTest(SaleTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Sale - Aggregates")]
    public void Instantiate()
    {
        var validItems = _fixture.GetValidItems();

        var sale = new Sale(validItems);

        sale.Should().NotBeNull();
        sale.Items.Should().HaveCount(1);
        sale.CreatedAt.Should().NotBe(default(DateTime));
        sale.TotalAmount.Should().Be(validItems.Sum(x => x.TotalAmount));
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenItemsIsEmpty))]
    [Trait("Domain", "Sale - Aggregates")]
    public void InstantiateErrorWhenItemsIsEmpty()
    {
        var emptyItems = new List<SaleItem>();

        Action act = () => new Sale(emptyItems);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("Sale must have at least one item");
    }

    [Fact(DisplayName = nameof(CalculateTotalAmount))]
    [Trait("Domain", "Sale - Aggregates")]
    public void CalculateTotalAmount()
    {
        var items = new List<SaleItem>
        {
            new(_fixture.GetValidProductId(), 2, 100.00m, new Warranty(50.00m)),
            new(_fixture.GetValidProductId(), 1, 200.00m, new Warranty(75.00m))
        };

        var sale = new Sale(items);

        // Item 1: (2 * 100) + 50 = 250
        // Item 2: (1 * 200) + 75 = 275
        // Total: 250 + 275 = 525
        sale.TotalAmount.Should().Be(525.00m);
    }
}

[Collection(nameof(SaleTestFixture))]
public class SaleItemTest
{
    private readonly SaleTestFixture _fixture;

    public SaleItemTest(SaleTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "SaleItem - Aggregates")]
    public void Instantiate()
    {
        var productId = _fixture.GetValidProductId();
        var quantity = _fixture.GetValidQuantity();
        var unitPrice = _fixture.GetValidUnitPrice();
        var warranty = _fixture.GetValidWarranty();

        var saleItem = new SaleItem(productId, quantity, unitPrice, warranty);

        saleItem.Should().NotBeNull();
        saleItem.ProductId.Should().Be(productId);
        saleItem.Quantity.Should().Be(quantity);
        saleItem.UnitPrice.Should().Be(unitPrice);
        saleItem.Warranty.Should().Be(warranty);
        saleItem.TotalAmount.Should().Be((quantity * unitPrice) + warranty.Value);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenQuantityIsInvalid))]
    [Trait("Domain", "SaleItem - Aggregates")]
    [InlineData(0)]
    [InlineData(-1)]
    public void InstantiateErrorWhenQuantityIsInvalid(int invalidQuantity)
    {
        Action act = () => new SaleItem(
            _fixture.GetValidProductId(),
            invalidQuantity,
            _fixture.GetValidUnitPrice(),
            _fixture.GetValidWarranty()
        );

        act.Should().Throw<DomainValidationException>()
            .WithMessage("Quantity must be greater than zero");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenUnitPriceIsInvalid))]
    [Trait("Domain", "SaleItem - Aggregates")]
    [InlineData(0)]
    [InlineData(-1)]
    public void InstantiateErrorWhenUnitPriceIsInvalid(decimal invalidUnitPrice)
    {
        Action act = () => new SaleItem(
            _fixture.GetValidProductId(),
            _fixture.GetValidQuantity(),
            invalidUnitPrice,
            _fixture.GetValidWarranty()
        );

        act.Should().Throw<DomainValidationException>()
            .WithMessage("Unit price must be greater than zero");
    }

    [Fact(DisplayName = nameof(CalculateTotalAmount))]
    [Trait("Domain", "SaleItem - Aggregates")]
    public void CalculateTotalAmount()
    {
        var quantity = 2;
        var unitPrice = 100.00m;
        var warrantyValue = 50.00m;

        var saleItem = new SaleItem(
            _fixture.GetValidProductId(),
            quantity,
            unitPrice,
            new Warranty(warrantyValue)
        );

        // (2 * 100) + 50 = 250
        saleItem.TotalAmount.Should().Be(250.00m);
    }
}