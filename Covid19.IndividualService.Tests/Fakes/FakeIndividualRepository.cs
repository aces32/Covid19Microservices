using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;

namespace Covid19.IndividualService.Tests.Fakes;

internal class FakeIndividualRepository : IIndividualRepository
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
