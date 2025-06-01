using MediatR;

namespace SaleMicroservice.API.Endpoints.Sales.Get;

public static class GetSaleEndpoint
{
    public static RouteGroupBuilder MapGetSaleEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id}", async (
                Guid id,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GetSaleQuery(id), cancellationToken);
                return Results.Ok(result);
            })
            .WithName("GetSale")
            .WithTags("Sales")
            .Produces<GetSaleResponse>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        return group;
    }
}

public record SaleItemResponse(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice,
    bool Warranty
);