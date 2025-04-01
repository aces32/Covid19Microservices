# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["Covid19.LocationService/Covid19.LocationService.csproj", "Covid19.LocationService/"]
COPY ["Covid19.Shared/Covid19.Shared.csproj", "Covid19.Shared/"]
RUN dotnet restore "./Covid19.LocationService/Covid19.LocationService.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/Covid19.LocationService
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Covid19.LocationService.dll", "--urls", "http://0.0.0.0:80"]