namespace ProductMicroservice.API.ApiModels.Product;

public record UpdateProductApiInput(
    string Name,
    decimal Price,
    int MinimumStock,
    int MaximumStock,
    int Stock,
    string Supplier,
    bool HasWarranty
);