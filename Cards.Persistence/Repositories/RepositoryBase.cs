using Cards.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cards.Persistence.Repositories
{
    public class RepositoryBase<T>(RepositoryDbContext repositoryDbContext) : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public IQueryable<T> InitializeQueryWithTrackingPreference(bool trackChanges)
        {
            if (trackChanges)
            {
                return _repositoryDbContext.Set<T>();
            }
            else
            {
                return _repositoryDbContext.Set<T>().AsNoTracking();
            }
        }
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            if (trackChanges)
            {
                return _repositoryDbContext.Set<T>().Where(expression);
            }
            else
            {
                return _repositoryDbContext.Set<T>().AsNoTracking().Where(expression);
            }
           
        }
        public async Task CreateAsync(T entity)
        {
            await _repositoryDbContext.Set<T>().AddAsync(entity);
        }
        public void Delete(T entity)
        {
            _repositoryDbContext.Set<T>().Remove(entity);
        }
    }
}
