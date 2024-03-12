using Cards.Domain.Contracts;

namespace Cards.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryDbContext _repositoryDbContext;
        public UnitOfWork(RepositoryDbContext repositoryDbContext)
        {
                _repositoryDbContext = repositoryDbContext;
        }
        public async Task SaveAsync()
        {
            await _repositoryDbContext.SaveChangesAsync();
        }
    }
}
