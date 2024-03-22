using Cards.Domain.Contracts;

namespace Cards.Persistence.Repositories
{
    public class UnitOfWork(RepositoryDbContext repositoryDbContext) : IUnitOfWork
    {
        public async Task SaveAsync()
        {
            await repositoryDbContext.SaveChangesAsync();
        }
    }
}
