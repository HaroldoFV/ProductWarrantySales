namespace ProductMicroservice.Application.UseCases.Product.Common;

public record ProductModelOutput(
    Guid Id,
    string Name,
    decimal Price,
    int MinimumStock,
    int MaximumStock,
    int Stock,
    string Supplier,
    bool HasWarranty
)
{
    public ProductModelOutput(Domain.Entity.Product product) : this(
        product.Id,
        product.Name,
        product.Price,
        product.MinimumStock,
        product.MaximumStock,
        product.Stock,
        product.Supplier,
        product.HasWarranty)
    {
    }
}