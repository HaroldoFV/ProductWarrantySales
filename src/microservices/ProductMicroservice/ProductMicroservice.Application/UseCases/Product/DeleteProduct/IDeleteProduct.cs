using MediatR;

namespace ProductMicroservice.Application.UseCases.Product.DeleteProduct;

public interface IDeleteProduct
    : IRequestHandler<DeleteProductInput>
{
}