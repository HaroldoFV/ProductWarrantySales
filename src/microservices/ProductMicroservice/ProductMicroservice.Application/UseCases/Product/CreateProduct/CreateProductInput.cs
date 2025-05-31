using MediatR;
using ProductMicroservice.Application.UseCases.Product.Common;

namespace ProductMicroservice.Application.UseCases.Product.CreateProduct;

public record CreateProductInput(
    string Name,
    decimal Price,
    int MinimumStock,
    int MaximumStock,
    int Stock,
    string Supplier,
    bool HasWarranty
) : IRequest<ProductModelOutput>;