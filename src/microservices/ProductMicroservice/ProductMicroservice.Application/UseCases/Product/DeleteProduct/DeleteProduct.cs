using ProductMicroservice.Application.Interfaces;
using ProductMicroservice.Domain.Repository;

namespace ProductMicroservice.Application.UseCases.Product.DeleteProduct;

public class DeleteProduct : IDeleteProduct
{
    public DeleteProduct(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;


    public async Task Handle(DeleteProductInput request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(request.Id, cancellationToken);

        await _productRepository.DeleteAsync(product, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}