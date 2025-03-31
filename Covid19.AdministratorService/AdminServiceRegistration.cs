using Covid19.AdministratorService.Core.Contracts.Infrastructure;
using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.AdministratorService.Infrastructure.Caching;
using Covid19.AdministratorService.Infrastructure.SyncDataService;
using Covid19.AdministratorService.Infrastructure.Validations;
using Covid19.AdministratorService.Persistence;
using Covid19.AdministratorService.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Http.Json;
using FluentValidation;
using MassTransit;
using Covid19.AdministratorService.Infrastructure.AsyncDataService;


namespace Covid19.AdministratorService
{
    public static class AdminServiceRegistration
    {
        public static IServiceCollection AddAdminServices(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddDistributedMemoryCache();
            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            _ = services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            _ = services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            _ = services.AddScoped<IAdminBookingRepository, AdminBookingRepository>();
            _ = services.AddHttpClient<ILocationServiceClient, LocationServiceClient>(client =>
                {
                    client.BaseAddress = new Uri($"{configuration["LocationService"]}"); 
                });
            _ = services.Configure<JsonOptions>(options =>
                {
                    options.SerializerOptions.PropertyNameCaseInsensitive = true;
                });
            _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            _ = services.AddSwaggerGen();
            _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            _ = services.AddGrpc();
            _ = services.AddGrpcReflection();

            Console.WriteLine("--> Using InMem DB");
            _ = services.AddDbContext<AdministratorDbContext>(options =>
                            options.UseInMemoryDatabase("InMemoryAdminDb"));
            AddSwagger(services);
            AddMassTransit(services, configuration);

            return services;
        }

        #region SwaggerImplementation
        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "COVID19.AdminServices.API", Version = "v1" });

                c.EnableAnnotations();

            });

            services.AddSwaggerGenNewtonsoftSupport();

        }
        #endregion

        #region MassTransitImplementation

        private static void AddMassTransit(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<SpaceAllocatedIncreasedEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host($"{configuration["RabbitMQHost"]}", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        #endregion

    }
}
