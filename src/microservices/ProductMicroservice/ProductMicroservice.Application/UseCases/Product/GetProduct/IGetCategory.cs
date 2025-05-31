using MediatR;
using ProductMicroservice.Application.UseCases.Product.Common;

namespace ProductMicroservice.Application.UseCases.Product.GetProduct;

public interface IGetCategory : IRequestHandler<GetProductInput, ProductModelOutput>;