using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Covid19.IndividualService.Persistence.Repository
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly IndividualDbContext _dbContext;
        public BaseRepository(IndividualDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> FindWhere(Expression<Func<T, bool>> filterExpression)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(filterExpression);
        }

        public virtual async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _dbContext.Set<T>().Where(filterExpression).ToListAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _dbContext.Set<T>().AnyAsync(filterExpression);
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<TProjected>> ListAllAsync<TProjected>(Expression<Func<T, TProjected>> projectionExpression)
        {
            return await _dbContext.Set<T>().Select(projectionExpression).ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> GetPagedResponseAsync(int page, int size)
        {
            return await _dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<T> AddAsync(T entity, bool saveCurrentChanges = true)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangesAsync(saveCurrentChanges);

            return entity;
        }

        public async Task<IReadOnlyList<T>> AddRangeAsync(IReadOnlyList<T> entity, bool saveCurrentChanges = true)
        {
            await _dbContext.Set<T>().AddRangeAsync(entity);
            await SaveChangesAsync(saveCurrentChanges);

            return entity;
        }

        public async Task<IReadOnlyList<T>> RemoveRangeAsync(IReadOnlyList<T> entity, bool saveCurrentChanges = true)
        {
            _dbContext.Set<T>().RemoveRange(entity);
            await SaveChangesAsync(saveCurrentChanges);

            return entity;
        }

        public async Task UpdateAsync(T entity, bool saveCurrentChanges = true)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync(saveCurrentChanges);
        }

        public async Task DeleteAsync(T entity, bool saveCurrentChanges = true)
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveChangesAsync(saveCurrentChanges);
        }

        public async Task SaveChangesAsync(bool saveCurrentChanges = true)
        {
            if (saveCurrentChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}