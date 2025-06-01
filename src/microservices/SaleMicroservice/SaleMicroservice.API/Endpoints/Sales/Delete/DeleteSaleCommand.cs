using MediatR;

namespace SaleMicroservice.API.Endpoints.Sales.Delete;

public record DeleteSaleCommand(Guid Id) : IRequest;