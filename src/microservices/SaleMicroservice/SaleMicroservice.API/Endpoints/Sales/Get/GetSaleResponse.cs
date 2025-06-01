namespace SaleMicroservice.API.Endpoints.Sales.Get;

public record GetSaleResponse(
    Guid Id,
    decimal TotalAmount,
    List<SaleItemResponse> Items
);