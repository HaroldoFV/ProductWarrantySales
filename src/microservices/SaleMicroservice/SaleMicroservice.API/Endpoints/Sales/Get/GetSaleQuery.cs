using MediatR;

namespace SaleMicroservice.API.Endpoints.Sales.Get;

 
public record GetSaleQuery(Guid Id) : IRequest<GetSaleResponse>;
