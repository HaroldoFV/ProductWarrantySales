using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Infra.Data.EF;

namespace ProductMicroservice.API.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<ProductWarrantySalesDbContext>(options =>
            options.UseInMemoryDatabase("ProductDb")
        );
        return services;
    }
}