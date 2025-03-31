using System.Linq.Expressions;

namespace Covid19.AdministratorService.Core.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        public Task<T?> GetByIdAsync(Guid id);
        public Task<T?> GetByIdAsync(int id);
        public Task<T?> FindWhere(Expression<Func<T, bool>> filterExpression);
        public Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> filterExpression);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> filterExpression);
        public Task<IEnumerable<T>> ListAllAsync();
        public Task<IEnumerable<TProjected>> ListAllAsync<TProjected>(Expression<Func<T, TProjected>> projectionExpression);
        public Task<IEnumerable<T>> GetPagedResponseAsync(int page, int size);
        public Task<T> AddAsync(T entity, bool saveCurrentChanges = true);
        public Task UpdateAsync(T entity, bool saveCurrentChanges = true);
        public Task DeleteAsync(T entity, bool saveCurrentChanges = true);
        public Task SaveChangesAsync(bool saveCurrentChanges = true);


    }
}