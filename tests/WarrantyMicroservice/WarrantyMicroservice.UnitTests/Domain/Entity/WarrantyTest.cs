using FluentAssertions;
using WarrantyMicroservice.Domain.Entity;
using WarrantyMicroservice.Domain.Exceptions;

namespace WarrantyMicroservice.UnitTests.Domain.Entity;

[Collection(nameof(WarrantyTestFixture))]
public class WarrantyTest
{
    private readonly WarrantyTestFixture _fixture;

    public WarrantyTest(WarrantyTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Warranty - Aggregates")]
    public void Instantiate()
    {
        var valid = _fixture.GetValidWarranty();

        var warranty = new Warranty(
            valid.Name,
            valid.Value,
            valid.TermInYears
        );

        warranty.Should().NotBeNull();
        warranty.Name.Should().Be(valid.Name);
        warranty.Value.Should().Be(valid.Value);
        warranty.TermInYears.Should().Be(valid.TermInYears);
        warranty.CreatedAt.Should().NotBe(default(DateTime));
        warranty.LastUpdated.Should().Be(default(DateTime));
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsInvalid))]
    [Trait("Domain", "Warranty - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void InstantiateErrorWhenNameIsInvalid(string? name)
    {
        var valid = _fixture.GetValidWarranty();
        Action act = () => new Warranty(
            name!,
            valid.Value,
            valid.TermInYears
        );

        act.Should().Throw<EntityValidationException>();
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenValueIsInvalid))]
    [Trait("Domain", "Warranty - Aggregates")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void InstantiateErrorWhenValueIsInvalid(decimal value)
    {
        var valid = _fixture.GetValidWarranty();
        Action act = () => new Warranty(
            valid.Name,
            value,
            valid.TermInYears
        );

        act.Should().Throw<EntityValidationException>();
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenTermInYearsIsInvalid))]
    [Trait("Domain", "Warranty - Aggregates")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void InstantiateErrorWhenTermInYearsIsInvalid(int termInYears)
    {
        var valid = _fixture.GetValidWarranty();
        Action act = () => new Warranty(
            valid.Name,
            valid.Value,
            termInYears
        );

        act.Should().Throw<EntityValidationException>();
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameLengthIsInvalid))]
    [Trait("Domain", "Warranty - Aggregates")]
    public void InstantiateErrorWhenNameLengthIsInvalid()
    {
        var valid = _fixture.GetValidWarranty();
        var tooShortName = "ab";
        var tooLongName = string.Join("", Enumerable.Repeat("a", 256));

        Action actTooShort = () => new Warranty(tooShortName, valid.Value, valid.TermInYears);
        Action actTooLong = () => new Warranty(tooLongName, valid.Value, valid.TermInYears);

        actTooShort.Should().Throw<EntityValidationException>();
        actTooLong.Should().Throw<EntityValidationException>();
    }

    [Fact(DisplayName = nameof(UpdateWarranty))]
    [Trait("Domain", "Warranty - Aggregates")]
    public void UpdateWarranty()
    {
        var warranty = _fixture.GetValidWarranty();
        var newName = "Premium Warranty";
        var newValue = 399.99m;
        var newTermInYears = 3;

        warranty.Update(newName, newValue, newTermInYears);

        warranty.Name.Should().Be(newName);
        warranty.Value.Should().Be(newValue);
        warranty.TermInYears.Should().Be(newTermInYears);
        warranty.LastUpdated.Should().NotBe(default(DateTime));
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsInvalid))]
    [Trait("Domain", "Warranty - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void UpdateErrorWhenNameIsInvalid(string? name)
    {
        var warranty = _fixture.GetValidWarranty();
        Action act = () => warranty.Update(
            name!,
            warranty.Value,
            warranty.TermInYears
        );

        act.Should().Throw<EntityValidationException>();
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenValueIsInvalid))]
    [Trait("Domain", "Warranty - Aggregates")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void UpdateErrorWhenValueIsInvalid(decimal value)
    {
        var warranty = _fixture.GetValidWarranty();
        Action act = () => warranty.Update(
            warranty.Name,
            value,
            warranty.TermInYears
        );

        act.Should().Throw<EntityValidationException>();
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenTermInYearsIsInvalid))]
    [Trait("Domain", "Warranty - Aggregates")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void UpdateErrorWhenTermInYearsIsInvalid(int termInYears)
    {
        var warranty = _fixture.GetValidWarranty();
        Action act = () => warranty.Update(
            warranty.Name,
            warranty.Value,
            termInYears
        );

        act.Should().Throw<EntityValidationException>();
    }
}