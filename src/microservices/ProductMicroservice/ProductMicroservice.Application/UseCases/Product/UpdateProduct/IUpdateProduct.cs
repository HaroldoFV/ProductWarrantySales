using MediatR;
using ProductMicroservice.Application.UseCases.Product.Common;

namespace ProductMicroservice.Application.UseCases.Product.UpdateProduct;
public interface IUpdateProduct
    : IRequestHandler<UpdateProductInput, ProductModelOutput>
{}
