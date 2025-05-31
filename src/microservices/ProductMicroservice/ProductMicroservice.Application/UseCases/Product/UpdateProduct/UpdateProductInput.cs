using MediatR;
using ProductMicroservice.Application.UseCases.Product.Common;

namespace ProductMicroservice.Application.UseCases.Product.UpdateProduct;

public record UpdateProductInput(
    Guid Id,
    string Name,
    decimal Price,
    int MinimumStock,
    int MaximumStock,
    int Stock,
    string Supplier,
    bool HasWarranty
) : IRequest<ProductModelOutput>;