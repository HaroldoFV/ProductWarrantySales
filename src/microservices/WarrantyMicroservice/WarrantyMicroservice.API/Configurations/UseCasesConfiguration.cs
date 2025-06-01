using WarrantyMicroservice.Application.Interfaces;
using WarrantyMicroservice.Application.UseCases.Warranty.CreateWarranty;
using WarrantyMicroservice.Domain.Repository;
using WarrantyMicroservice.Infra.Data.EF;
using WarrantyMicroservice.Infra.Data.EF.Repositories;

namespace WarrantyMicroservice.API.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateWarranty).Assembly)
        );
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<IWarrantyRepository, WarrantyRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}