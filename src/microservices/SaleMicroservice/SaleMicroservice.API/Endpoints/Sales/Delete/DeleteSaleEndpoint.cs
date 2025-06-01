using MediatR;

namespace SaleMicroservice.API.Endpoints.Sales.Delete;

public static class DeleteSaleEndpoint
{
    public static RouteGroupBuilder MapDeleteSaleEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id}", async (
                Guid id,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                await mediator.Send(new DeleteSaleCommand(id), cancellationToken);
                return Results.NoContent();
            })
            .WithName("DeleteSale")
            .WithTags("Sales")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        return group;
    }
}