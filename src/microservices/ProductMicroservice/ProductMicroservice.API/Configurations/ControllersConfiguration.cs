using Microsoft.OpenApi.Models;
using ProductMicroservice.API.Filters;

namespace ProductMicroservice.API.Configurations;

public static class ControllersConfiguration
{
    public static IServiceCollection AddAndConfigureControllers(
        this IServiceCollection services
    )
    {
        services
            .AddControllers(options
                => options.Filters.Add(typeof(ApiGlobalExceptionFilter))
            );
        services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
        services.AddDocumentation();
        return services;
    }

    private static IServiceCollection AddDocumentation(
        this IServiceCollection services
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "FC3 Codeflix Catalog", Version = "v1" });
        });
        return services;
    }

    public static WebApplication UseDocumentation(
        this WebApplication app
    )
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}