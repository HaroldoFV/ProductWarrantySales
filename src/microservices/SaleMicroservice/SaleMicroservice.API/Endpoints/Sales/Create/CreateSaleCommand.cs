using MediatR;
using SalesMicroservice.API.Endpoints.Sales.Create;

namespace SaleMicroservice.API.Endpoints.Sales.Create;

public record CreateSaleCommand(CreateSaleRequest Request) : IRequest<CreateSaleResponse>;