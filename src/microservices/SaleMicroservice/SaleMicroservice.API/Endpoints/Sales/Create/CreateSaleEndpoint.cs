using MediatR;
using SalesMicroservice.API.Endpoints.Sales.Create;

namespace SaleMicroservice.API.Endpoints.Sales.Create;

public static class CreateSaleEndpoint
{
    public static RouteGroupBuilder MapCreateSaleEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (
                CreateSaleRequest request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateSaleCommand(request);
                var result = await mediator.Send(command, cancellationToken);

                return Results.Created($"/sales/{result.Id}", result);
            })
            .WithName("CreateSale")
            .WithOpenApi()
            .WithTags("Sales");

        return group;
    }
}