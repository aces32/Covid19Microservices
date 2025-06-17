using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.AdministratorService.Domain.Entities;

namespace Covid19.AdministratorService.Tests.Fakes;

internal class FakeAdminBookingRepository : IAdminBookingRepository
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
