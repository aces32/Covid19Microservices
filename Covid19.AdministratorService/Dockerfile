# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["Covid19.AdministratorService/Covid19.AdministratorService.csproj", "Covid19.AdministratorService/"]
COPY ["Covid19.Shared/Covid19.Shared.csproj", "Covid19.Shared/"]
RUN dotnet restore "./Covid19.AdministratorService/Covid19.AdministratorService.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/Covid19.AdministratorService
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Covid19.AdministratorService.dll", "--urls", "http://0.0.0.0:80"]
