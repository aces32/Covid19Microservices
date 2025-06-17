using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;

namespace Covid19.IndividualService.Tests.Fakes;

internal class FakeBookingAvailabilityRepository : IBookingAvailabilityRepository
{
    private readonly BookingAvailability? _availability;

    public FakeBookingAvailabilityRepository(BookingAvailability? availability) => _availability = availability;

    public Task<BookingAvailability?> FindWhere(System.Linq.Expressions.Expression<Func<BookingAvailability, bool>> filterExpression)
    {
        var func = filterExpression.Compile();
        return Task.FromResult(_availability != null && func(_availability) ? _availability : null);
    }

    public Task UpdateAsync(BookingAvailability entity, bool saveCurrentChanges = true) => Task.CompletedTask;
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
