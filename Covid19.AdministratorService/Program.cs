using Covid19.AdministratorService;
using Covid19.AdministratorService.Infrastructure.Logging;
using Covid19.AdministratorService.Infrastructure.SyncDataService;
using Covid19.AdministratorService.Persistence.Repository;
using Covid19.Shared.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.
builder.Services.AddAdminServices(configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "COVID19.AdminServices.Api v1"));
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html", permanent: false);
    return Task.CompletedTask;
});

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<BookingAvailabilityGrpcService>();
app.MapGrpcReflectionService();

app.PrepPopulation();

app.Run();
