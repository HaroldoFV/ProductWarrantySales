using FluentValidation;

namespace ProductMicroservice.Application.UseCases.Product.UpdateProduct;

public class UpdateProductInputValidator
    : AbstractValidator<UpdateProductInput>
{
    public UpdateProductInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}