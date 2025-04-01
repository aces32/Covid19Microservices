using Covid19.IndividualLabsService.Core.Contracts.Persistence;
using Covid19.IndividualLabsService.Infrastructure.AsyncDataService;
using Covid19.IndividualLabsService.Infrastructure.Validations;
using Covid19.IndividualLabsService.Persistence;
using Covid19.IndividualLabsService.Persistence.Repository;
using Covid19.Shared.EventContracts;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Covid19.IndividualLabsService
{
    public static class IndividualLabsRegistration
    {
        public static IServiceCollection AddIndividualLabsServices(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            _ = services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            _ = services.AddScoped<IIndividualLabRepository, IndividualLabRepository>();
            _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            _ = services.AddSwaggerGen();
            _ = services.AddControllers()
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        });

            Console.WriteLine("--> Using InMem DB");
            _ = services.AddDbContext<IndividualLabsDbContext>(options =>
                            options.UseInMemoryDatabase("InMemoryDb"));
            AddSwagger(services);
            AddMassTransit(services, configuration);

            return services;
        }

        #region SwaggerImplementation
        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "COVID19.IndividualLabsService.API", Version = "v1" });

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
                x.AddConsumer<CovidTestBookedEventConsumer>();
                x.AddConsumer<CovidTestCancelledEventConsumer>();

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
