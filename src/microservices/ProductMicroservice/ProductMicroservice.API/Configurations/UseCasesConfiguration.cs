using ProductMicroservice.Application.Interfaces;
using ProductMicroservice.Application.UseCases.Product.CreateProduct;
using ProductMicroservice.Domain.Repository;
using ProductMicroservice.Infra.Data.EF;
using ProductMicroservice.Infra.Data.EF.Repositories;

namespace ProductMicroservice.API.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateProduct).Assembly)
        );
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}