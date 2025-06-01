using FluentValidation;

namespace WarrantyMicroservice.Application.UseCases.Warranty.GetWarranty;

public class GeWarrantyInputValidator
    : AbstractValidator<GetWarrantyInput>
{
    public GeWarrantyInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}