using Cards.Domain.Contracts;
using Cards.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cards.Persistence.Repositories
{
    public class AppUserRepository(UserManager<AppUser> userManager) : IAppUserRepository 
    {
        public async Task<AppUser?> GetAppUserByIdAsync(string appUserId, bool trackChanges)
        {
            AppUser? appUser;

            if (trackChanges)
            {
                appUser = await userManager.Users
                        .Include(appUser => appUser.Role)
                        .FirstOrDefaultAsync(appUser => appUser.Id == appUserId);
            }
            else
            {
                appUser = await userManager.Users
                        .AsNoTracking()
                        .Include(appUser => appUser.Role)
                        .FirstOrDefaultAsync(appUser => appUser.Id == appUserId);
            }

            return appUser;
        }
    }
}
