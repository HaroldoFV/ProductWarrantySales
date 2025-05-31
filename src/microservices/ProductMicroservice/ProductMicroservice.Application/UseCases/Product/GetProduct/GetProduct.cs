using ProductMicroservice.Application.UseCases.Product.Common;
using ProductMicroservice.Domain.Repository;

namespace ProductMicroservice.Application.UseCases.Product.GetProduct;

public class GetProduct : IGetCategory
{
    private readonly IProductRepository _productRepository;

    public GetProduct(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductModelOutput> Handle(
        GetProductInput request,
        CancellationToken cancellationToken
    )
    {
        var product = await _productRepository.GetAsync(request.Id, cancellationToken);
        return new ProductModelOutput(product);
    }
}