using ProductMicroservice.Application.Interfaces;
using ProductMicroservice.Application.UseCases.Product.Common;
using ProductMicroservice.Domain.Repository;
using DomainEntity = ProductMicroservice.Domain.Entity;


namespace ProductMicroservice.Application.UseCases.Product.CreateProduct;

public class CreateProduct : ICreateProduct
{
    public CreateProduct(IProductRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IProductRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;


    public async Task<ProductModelOutput> Handle(
        CreateProductInput input,
        CancellationToken cancellationToken)
    {
        var product = new DomainEntity.Product(
            input.Name,
            input.Price,
            input.MinimumStock,
            input.MaximumStock,
            input.Stock,
            input.Supplier,
            input.HasWarranty
        );

        await _categoryRepository.InsertAsync(product, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return new ProductModelOutput(product);
    }
}