FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/microservices/ProductMicroservice/ProductMicroservice.API/ProductMicroservice.API.csproj", "src/microservices/ProductMicroservice/ProductMicroservice.API/"]
COPY ["src/microservices/ProductMicroservice/ProductMicroservice.Infra.Data.EF/ProductMicroservice.Infra.Data.EF.csproj", "src/microservices/ProductMicroservice/ProductMicroservice.Infra.Data.EF/"]
COPY ["src/microservices/ProductMicroservice/ProductMicroservice.Domain/ProductMicroservice.Domain.csproj", "src/microservices/ProductMicroservice/ProductMicroservice.Domain/"]
COPY ["src/microservices/ProductMicroservice/ProductMicroservice.Application/ProductMicroservice.Application.csproj", "src/microservices/ProductMicroservice/ProductMicroservice.Application/"]
RUN dotnet restore "src/microservices/ProductMicroservice/ProductMicroservice.API/ProductMicroservice.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/ProductMicroservice/ProductMicroservice.API"
RUN dotnet build "./ProductMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductMicroservice.API.dll"]
