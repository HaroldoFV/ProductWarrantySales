FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/microservices/SaleMicroservice/SaleMicroservice.API/SaleMicroservice.API.csproj", "src/microservices/SaleMicroservice/SaleMicroservice.API/"]
RUN dotnet restore "src/microservices/SaleMicroservice/SaleMicroservice.API/SaleMicroservice.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/SaleMicroservice/SaleMicroservice.API"
RUN dotnet build "./SaleMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SaleMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SaleMicroservice.API.dll"]
