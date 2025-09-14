## Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY perla-metro-users-service.csproj ./
RUN dotnet restore ./perla-metro-users-service.csproj

# copy everything else and build
COPY . .
RUN dotnet publish ./perla-metro-users-service.csproj -c Release -o /app/publish /p:UseAppHost=false

## Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Use 8080 as the default container port; can be overridden at runtime
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "perla-metro-users-service.dll"]

