using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleMicroservice.API.Common.Behaviors;
using SaleMicroservice.API.Common.Exceptions;
using SaleMicroservice.API.Common.Infrastructure.Persistence;
using SaleMicroservice.API.Endpoints.Sales;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

// Validators
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Database
builder.Services.AddDbContext<SalesDbContext>(options => { options.UseInMemoryDatabase("SalesDb"); });

// Add Behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


var app = builder.Build();

// app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(builder => builder.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    var (statusCode, error) = exception switch
    {
        ValidationException validationEx => (StatusCodes.Status400BadRequest,
            new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Failed",
                Detail = "One or more validation errors occurred",
                Extensions =
                {
                    ["errors"] = validationEx.Errors.Select(e => e.ErrorMessage).ToArray()
                }
            }),
        NotFoundException => (StatusCodes.Status404NotFound,
            new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            }),
        _ => (StatusCodes.Status500InternalServerError,
            new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An error occurred"
            })
    };

    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/problem+json";

    await context.Response.WriteAsJsonAsync(error);
}));


// Endpoints
app.MapSalesEndpoints();


app.Run();