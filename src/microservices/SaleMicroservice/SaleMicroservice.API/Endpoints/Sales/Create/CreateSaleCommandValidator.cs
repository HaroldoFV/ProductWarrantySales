using FluentValidation;

namespace SaleMicroservice.API.Endpoints.Sales.Create;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleForEach(x => x.Request.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Warranty!.Value)
                    .GreaterThan(0)
                    .WithMessage("Warranty value must be greater than zero");
            });
    }
}