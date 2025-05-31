using MediatR;

namespace ProductMicroservice.Application.UseCases.Product.ListProducts;
public interface IListProducts
    : IRequestHandler<ListProductsInput, ListProductsOutput>
{}
