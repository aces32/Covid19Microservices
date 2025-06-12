using Covid19.AdministratorService.Core.Features.Administrators.Queries.GetAvailableBookingDetails;
using Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetLocationsByIdsHandler;
using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.AdministratorService.Models.LocationModels;
using Covid19.AdministratorService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Covid19.AdministratorService.Tests
{
    public class GetAvailableBookingDetailsQueryHandlerTests
    {
        private class FakeAdminBookingRepository : IAdminBookingRepository
        {
            private readonly IEnumerable<AdminBookingAllocation> _items;
            public FakeAdminBookingRepository(IEnumerable<AdminBookingAllocation> items) => _items = items;
            public Task<IEnumerable<AdminBookingAllocation>> ListAllAsync() => Task.FromResult(_items);
            // unused interface members
            public Task<AdminBookingAllocation> AddAsync(AdminBookingAllocation entity, bool saveCurrentChanges = true) => throw new NotImplementedException();
            public Task DeleteAsync(AdminBookingAllocation entity, bool saveCurrentChanges = true) => throw new NotImplementedException();
            public Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<AdminBookingAllocation, bool>> filterExpression) => throw new NotImplementedException();
            public Task<AdminBookingAllocation?> FindWhere(System.Linq.Expressions.Expression<Func<AdminBookingAllocation, bool>> filterExpression) => throw new NotImplementedException();
            public Task<IEnumerable<AdminBookingAllocation>> GetPagedResponseAsync(int page, int size) => throw new NotImplementedException();
            public Task<AdminBookingAllocation?> GetByIdAsync(int id) => throw new NotImplementedException();
            public Task<AdminBookingAllocation?> GetByIdAsync(Guid id) => throw new NotImplementedException();
            public Task<IEnumerable<TProjected>> ListAllAsync<TProjected>(System.Linq.Expressions.Expression<Func<AdminBookingAllocation, TProjected>> projectionExpression) => throw new NotImplementedException();
            public Task<IEnumerable<AdminBookingAllocation>> WhereAsync(System.Linq.Expressions.Expression<Func<AdminBookingAllocation, bool>> filterExpression) => throw new NotImplementedException();
            public Task SaveChangesAsync(bool saveCurrentChanges = true) => throw new NotImplementedException();
            public Task UpdateAsync(AdminBookingAllocation entity, bool saveCurrentChanges = true) => throw new NotImplementedException();
        }

        private class FakeMediator : IMediator
        {
            private readonly List<AvailableLocationListDto> _locations;
            public FakeMediator(List<AvailableLocationListDto> locations) => _locations = locations;
            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
            {
                if (request is GetLocationsByIdsQuery)
                {
                    return Task.FromResult((TResponse)(object)_locations);
                }
                throw new NotImplementedException();
            }
            // unused members
            public Task<object?> Send(object request, CancellationToken cancellationToken = default) => throw new NotImplementedException();
            public Task Publish(object notification, CancellationToken cancellationToken = default) => Task.CompletedTask;
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification => Task.CompletedTask;
            public Task Publish<TNotification>(TNotification notification, PublishOptions options, CancellationToken cancellationToken = default) where TNotification : INotification => Task.CompletedTask;
        }

        private class NoOpLogger<T> : ILogger<T>
        {
            public IDisposable BeginScope<TState>(TState state) => null!;
            public bool IsEnabled(LogLevel logLevel) => false;
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
        }

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
