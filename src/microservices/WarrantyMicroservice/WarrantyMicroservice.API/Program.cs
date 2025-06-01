using WarrantyMicroservice.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpLogging(_ => { })
    .AddAppConnections(builder.Configuration)
    .AddUseCases()
    .AddAndConfigureControllers()
    .AddCors(p => p.AddPolicy("CORS", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));


var app = builder.Build();
app.UseHttpLogging();
app.UseDocumentation();
app.UseCors("CORS");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program
{
}