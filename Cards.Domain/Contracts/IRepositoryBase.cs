using Cards.Domain.Entities;
using System.Linq.Expressions;

namespace Cards.Domain.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        public abstract IQueryable<T>  InitializeQueryWithTrackingPreference(bool trackChanges);
        public abstract IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        public abstract Task CreateAsync(T entity);
        public abstract void Delete(T entity);
        public abstract void Detach(T entity);
    }
}
