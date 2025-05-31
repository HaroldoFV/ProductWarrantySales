using MediatR;
using ProductMicroservice.Application.UseCases.Product.Common;

namespace ProductMicroservice.Application.UseCases.Product.GetProduct;

public class GetProductInput : IRequest<ProductModelOutput>
{
    public Guid Id { get; set; }

    public GetProductInput(Guid id)
        => Id = id;
}