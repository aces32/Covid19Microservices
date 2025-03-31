using COVID_19PCR.TestManagement.Persistence.Repository;
using Covid19.GrpcContracts;
using Covid19.IndividualService.Core.Contracts.Infrastructure;
using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Infrastructure.AsyncDataService;
using Covid19.IndividualService.Infrastructure.SyncDataService;
using Covid19.IndividualService.Infrastructure.Validations;
using Covid19.IndividualService.Persistence;
using Covid19.IndividualService.Persistence.Repository;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Covid19.IndividualService
{
    public static class IndividualServiceRegistration
    {
        public static IServiceCollection AddIndividualServices(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            _ = services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            _ = services.AddScoped<IIndividualRepository, IndividualRepository>();
            _ = services.AddScoped<IBookingAvailabilityRepository, BookingAvailabilityRepository>();
            _ = services.AddScoped<IBookingAvailabilitySyncService, BookingAvailabilityGrpcService>();
            _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            _ = services.AddSwaggerGen();
            _ = services.AddControllers()
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        });
            _ = services.AddGrpcClient<BookingAvailabilityService.BookingAvailabilityServiceClient>(o =>
                         {
                             o.Address = new Uri($"{configuration["GrpcPlatform"]}"); 
                         });

            Console.WriteLine("--> Using InMem DB");
            _ = services.AddDbContext<IndividualDbContext>(options =>
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

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "COVID19.IndividualService.API", Version = "v1" });

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
                x.AddConsumer<BookingSlotCreatedEventConsumer>();

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
