using FluentValidation;

namespace SaleMicroservice.API.Endpoints.Sales.Create;

public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one item is required");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.ProductId)
                .NotEmpty();

            item.RuleFor(x => x.Quantity)
                .GreaterThan(0);

            item.RuleFor(x => x.UnitPrice)
                .GreaterThan(0);
        });
    }
}