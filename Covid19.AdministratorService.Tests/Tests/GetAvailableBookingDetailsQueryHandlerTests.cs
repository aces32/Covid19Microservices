using Covid19.AdministratorService.Core.Features.Administrators.Queries.GetAvailableBookingDetails;
using Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetLocationsByIdsHandler;
using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.AdministratorService.Models.LocationModels;
using Covid19.AdministratorService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using Covid19.AdministratorService.Tests.Fakes;
using Xunit;

namespace Covid19.AdministratorService.Tests
{
    public class GetAvailableBookingDetailsQueryHandlerTests
    {

        [Fact]
        public async Task Handle_ReturnsBookingDetailsWithLocations()
        {
            var bookings = new[]
            {
                new AdminBookingAllocation { BookingDate = DateTimeOffset.Parse("2024-01-01"), Capacity = 10, SpaceAllocated = 5, LocationId = 1 },
                new AdminBookingAllocation { BookingDate = DateTimeOffset.Parse("2024-01-02"), Capacity = 20, SpaceAllocated = 7, LocationId = 2 }
            };
            var repo = new FakeAdminBookingRepository(bookings);
            var mediator = new FakeMediator(new()
            {
                new AvailableLocationListDto { LocationID = 1, LocationName = "Loc1" },
                new AvailableLocationListDto { LocationID = 2, LocationName = "Loc2" }
            });
            var logger = new NoOpLogger<GetAvailableBookingDetailsQueryHandler>();
            var handler = new GetAvailableBookingDetailsQueryHandler(repo, logger, mediator);

            var result = await handler.Handle(new GetAvailableBookingDetailsQuery(), CancellationToken.None);

            Assert.Equal(2, result.Count);
            Assert.Equal("Loc1", result[0].Location.LocationName);
            Assert.Equal(5, result[0].SpaceAllocated);
        }
    }
}
