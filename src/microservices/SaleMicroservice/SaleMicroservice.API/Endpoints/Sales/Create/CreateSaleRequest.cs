namespace SaleMicroservice.API.Endpoints.Sales.Create;

public record CreateSaleRequest
{
    public required List<CreateSaleItemRequest> Items { get; init; }
}

public record CreateSaleItemRequest
{
    public required Guid ProductId { get; init; }
    public required int Quantity { get; init; }
    public required decimal UnitPrice { get; init; }
    public CreateWarrantyRequest? Warranty { get; init; }
}

public record CreateWarrantyRequest
{
    public required Guid WarrantyId { get; init; }
    public required decimal Value { get; init; }
}