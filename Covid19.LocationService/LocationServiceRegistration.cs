using Covid19.LocationService.Core.Contracts.Persistence;
using Covid19.LocationService.Infrastructure.Validations;
using Covid19.LocationService.Persistence;
using Covid19.LocationService.Persistence.Repository;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Covid19.LocationService
{
    public static class LocationServiceRegistration
    {
        public static IServiceCollection AddLocationServices(this IServiceCollection services)
        {
            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            _ = services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            _ = services.AddScoped<ILocationRepository, LocationRepository>();
            _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            _ = services.AddSwaggerGen();

            Console.WriteLine("--> Using InMem DB");
            _ = services.AddDbContext<LocationDbContext>(options =>
                            options.UseInMemoryDatabase("InMemoryDb"));
            AddSwagger(services);

            return services;
        }

        #region SwaggerImplementation
        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "COVID19.LocationServices.API", Version = "v1" });

                c.EnableAnnotations();

            });

            services.AddSwaggerGenNewtonsoftSupport();

        }
        #endregion

    }
}
