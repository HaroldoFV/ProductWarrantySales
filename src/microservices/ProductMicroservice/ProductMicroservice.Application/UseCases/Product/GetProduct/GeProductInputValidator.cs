
using FluentValidation;

namespace ProductMicroservice.Application.UseCases.Product.GetProduct;
public class GeProductInputValidator 
    : AbstractValidator<GetProductInput>
{
    public GeProductInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}
