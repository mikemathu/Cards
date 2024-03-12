using Cards.Domain.Contracts;
using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cards.Persistence.Repositories
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository 
    {
        public AppUserRepository(RepositoryDbContext repositoryDbContext)
            :base(repositoryDbContext)
        {            
        }
        public async Task<AppUser?> GetAppUserByIdAsync(int appUserId, bool trackChanges)
        {
            var user = await GetByCondition(appUser => appUser.AppUserId == appUserId, trackChanges)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
