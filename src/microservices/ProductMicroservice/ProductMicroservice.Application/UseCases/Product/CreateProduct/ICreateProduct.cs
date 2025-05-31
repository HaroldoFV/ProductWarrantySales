using MediatR;
using ProductMicroservice.Application.UseCases.Product.Common;

namespace ProductMicroservice.Application.UseCases.Product.CreateProduct;

public interface ICreateProduct :
    IRequestHandler<CreateProductInput, ProductModelOutput>
{
}