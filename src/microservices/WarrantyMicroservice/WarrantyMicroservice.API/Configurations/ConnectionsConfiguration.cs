using Microsoft.EntityFrameworkCore;
using WarrantyMicroservice.Infra.Data.EF;

namespace WarrantyMicroservice.API.Configurations;

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
        services.AddDbContext<WarrantyDbContext>(options =>
            options.UseInMemoryDatabase("WarrantyDb")
        );
        return services;
    }
}