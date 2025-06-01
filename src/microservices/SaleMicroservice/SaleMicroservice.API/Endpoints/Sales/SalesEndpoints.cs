using SaleMicroservice.API.Endpoints.Sales.Create;
using SaleMicroservice.API.Endpoints.Sales.Delete;
using SaleMicroservice.API.Endpoints.Sales.Get;

namespace SaleMicroservice.API.Endpoints.Sales;

public static class SalesEndpoints
{
    public static IEndpointRouteBuilder MapSalesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sales");

        group.MapCreateSaleEndpoint()
            .MapDeleteSaleEndpoint()
            .MapGetSaleEndpoint();

        return app;
    }
}