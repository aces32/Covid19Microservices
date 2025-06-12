using Covid19.LocationService.Core.Dto;
using Covid19.LocationService.Core.Features.Locations.Queries.GetAllAvailableLocations;
using Covid19.LocationService.Persistence;
using Covid19.LocationService.Persistence.Repository;
using Covid19.LocationService.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Covid19.LocationService.Tests
{
    public class GetAllAvailableLocationsQueryHandlerTests
    {
        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Covid19.LocationService.Core.Profiles.MappingProfiles>());
            return config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ReturnsAllLocationsOrderedById()
        {
            var options = new DbContextOptionsBuilder<LocationDbContext>()
                .UseInMemoryDatabase(databaseName: "locations_db")
                .Options;
            using var context = new LocationDbContext(options);
            context.Locations.AddRange(
                new Location { LocationID = 2, LocationName = "B" },
                new Location { LocationID = 1, LocationName = "A" }
            );
            context.SaveChanges();

            var repository = new LocationRepository(context);
            var logger = new TestLogger<GetAllAvailableLocationsQueryHandler>();
            var handler = new GetAllAvailableLocationsQueryHandler(CreateMapper(), repository, logger);

            var result = await handler.Handle(new GetAllAvailableLocationsQuery(), CancellationToken.None);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].LocationID);
            Assert.Equal("A", result[0].LocationName);
        }

        private class TestLogger<T> : ILogger<T>
        {
            public IDisposable BeginScope<TState>(TState state) => null!;
            public bool IsEnabled(LogLevel logLevel) => false;
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
        }
    }
}
