using Covid19.IndividualService.Core.Features.Command.BookCovidTest;
using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;
using Covid19.Shared.Exceptions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MediatR;
using MassTransit;
using Xunit;

namespace Covid19.IndividualService.Tests
{
    public class BookCovidTestCommandHandlerTests
    {
        private class FakeBookingAvailabilityRepository : IBookingAvailabilityRepository
        {
            private readonly BookingAvailability? _availability;
            public FakeBookingAvailabilityRepository(BookingAvailability? availability) => _availability = availability;
            public Task<BookingAvailability?> FindWhere(System.Linq.Expressions.Expression<Func<BookingAvailability, bool>> filterExpression)
            {
                var func = filterExpression.Compile();
                return Task.FromResult(_availability != null && func(_availability) ? _availability : null);
            }
            public Task UpdateAsync(BookingAvailability entity, bool saveCurrentChanges = true) { return Task.CompletedTask; }
            public Task<BookingAvailability?> GetByIdAsync(Guid id) => throw new NotImplementedException();
            public Task<BookingAvailability?> GetByIdAsync(int id) => throw new NotImplementedException();
            public Task<IEnumerable<BookingAvailability>> ListAllAsync() => throw new NotImplementedException();
            public Task<BookingAvailability> AddAsync(BookingAvailability entity, bool saveCurrentChanges = true) => Task.FromResult(entity);
            public Task DeleteAsync(BookingAvailability entity, bool saveCurrentChanges = true) => throw new NotImplementedException();
            public Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<BookingAvailability, bool>> filterExpression) => throw new NotImplementedException();
            public Task<IEnumerable<BookingAvailability>> GetPagedResponseAsync(int page, int size) => throw new NotImplementedException();
            public Task<IEnumerable<BookingAvailability>> WhereAsync(System.Linq.Expressions.Expression<Func<BookingAvailability, bool>> filterExpression) => throw new NotImplementedException();
            public Task<IEnumerable<TProjected>> ListAllAsync<TProjected>(System.Linq.Expressions.Expression<Func<BookingAvailability, TProjected>> projectionExpression) => throw new NotImplementedException();
            public Task SaveChangesAsync(bool saveCurrentChanges = true) => Task.CompletedTask;
        }
        private class FakeIndividualRepository : IIndividualRepository
        {
            public Task<Individual> AddAsync(Individual entity, bool saveCurrentChanges = true) => Task.FromResult(entity);
            public Task<Individual?> FindWhere(System.Linq.Expressions.Expression<Func<Individual, bool>> filterExpression) => Task.FromResult<Individual?>(null);
            public Task UpdateAsync(Individual entity, bool saveCurrentChanges = true) => Task.CompletedTask;
            public Task<Individual?> GetByIdAsync(Guid id) => throw new NotImplementedException();
            public Task<Individual?> GetByIdAsync(int id) => throw new NotImplementedException();
            public Task<IEnumerable<Individual>> ListAllAsync() => throw new NotImplementedException();
            public Task DeleteAsync(Individual entity, bool saveCurrentChanges = true) => throw new NotImplementedException();
            public Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<Individual, bool>> filterExpression) => Task.FromResult(false);
            public Task<IEnumerable<Individual>> GetPagedResponseAsync(int page, int size) => throw new NotImplementedException();
            public Task<IEnumerable<Individual>> WhereAsync(System.Linq.Expressions.Expression<Func<Individual, bool>> filterExpression) => throw new NotImplementedException();
            public Task<IEnumerable<TProjected>> ListAllAsync<TProjected>(System.Linq.Expressions.Expression<Func<Individual, TProjected>> projectionExpression) => throw new NotImplementedException();
            public Task SaveChangesAsync(bool saveCurrentChanges = true) => Task.CompletedTask;
        }
        private class NoOpPublishEndpoint : IPublishEndpoint
        {
            public List<object> Published { get; } = new();
            public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
            {
                Published.Add(message!);
                return Task.CompletedTask;
            }
            // other methods not used
            public Task Publish(object message, CancellationToken cancellationToken = default) => Task.CompletedTask;
            public Task Publish(object message, Type messageType, CancellationToken cancellationToken = default) => Task.CompletedTask;
        }
        private class NoOpLogger<T> : ILogger<T>
        {
            public IDisposable BeginScope<TState>(TState state) => null!;
            public bool IsEnabled(LogLevel logLevel) => false;
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
        }
        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Covid19.IndividualService.Core.Profiles.MappingProfiles>());
            return config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ThrowsNotFound_WhenBookingAvailabilityMissing()
        {
            var command = new BookCovidTestCommand
            {
                LocationId = 1,
                BookingDate = DateTime.UtcNow,
                IndividualEmailAddress = "a@test.com",
                IndividualFirstName = "A",
                IndividualLastName = "B",
                IndividualMobileNumber = "123",
                IndividualLab = new IndividualLabsRequest{ TestType = TestType.PCRTest }
            };
            var handler = new BookCovidTestCommandHandler(CreateMapper(), new NoOpLogger<BookCovidTestCommandHandler>(),
                new FakeIndividualRepository(), new FakeBookingAvailabilityRepository(null), new NoOpPublishEndpoint());

            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}

