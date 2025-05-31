using ProductMicroservice.Application.Interfaces;
using ProductMicroservice.Application.UseCases.Product.Common;
using ProductMicroservice.Domain.Repository;

namespace ProductMicroservice.Application.UseCases.Product.UpdateProduct;
public class UpdateProduct : IUpdateProduct
{
    public UpdateProduct(IProductRepository productRepository, IUnitOfWork uinitOfWork)
    {
        _productRepository = productRepository;
        _uinitOfWork = uinitOfWork;
    }

    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uinitOfWork;
 

    public async Task<ProductModelOutput> Handle(UpdateProductInput request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(request.Id, cancellationToken);
        product.Update(
            request.Name,
            request.Price,
            request.MinimumStock,
            request.MaximumStock,
            request.Stock,
            request.Supplier,
            request.HasWarranty
        );
 
        await _productRepository.UpdateAsync(product, cancellationToken);
        await _uinitOfWork.Commit(cancellationToken);
        return new ProductModelOutput(product);
    }
}
