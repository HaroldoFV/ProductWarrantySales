using FluentValidation;

namespace WarrantyMicroservice.Application.UseCases.Warranty.UpdateWarranty;

public class UpdateWarrantyInputValidator
    : AbstractValidator<UpdateWarrantyInput>
{
    public UpdateWarrantyInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}