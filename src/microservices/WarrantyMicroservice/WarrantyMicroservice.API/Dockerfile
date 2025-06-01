FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/microservices/WarrantyMicroservice/WarrantyMicroservice.API/WarrantyMicroservice.API.csproj", "src/microservices/WarrantyMicroservice/WarrantyMicroservice.API/"]
COPY ["src/microservices/WarrantyMicroservice/WarrantyMicroservice.Infra.Data.EF/WarrantyMicroservice.Infra.Data.EF.csproj", "src/microservices/WarrantyMicroservice/WarrantyMicroservice.Infra.Data.EF/"]
COPY ["src/microservices/WarrantyMicroservice/WarrantyMicroservice.Application/WarrantyMicroservice.Application.csproj", "src/microservices/WarrantyMicroservice/WarrantyMicroservice.Application/"]
COPY ["src/microservices/WarrantyMicroservice/WarrantyMicroservice.Domain/WarrantyMicroservice.Domain.csproj", "src/microservices/WarrantyMicroservice/WarrantyMicroservice.Domain/"]
RUN dotnet restore "src/microservices/WarrantyMicroservice/WarrantyMicroservice.API/WarrantyMicroservice.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/WarrantyMicroservice/WarrantyMicroservice.API"
RUN dotnet build "./WarrantyMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WarrantyMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarrantyMicroservice.API.dll"]
