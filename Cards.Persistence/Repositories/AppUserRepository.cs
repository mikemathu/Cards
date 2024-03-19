using Cards.Domain.Contracts;
using Cards.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cards.Persistence.Repositories
{
    public class AppUserRepository : IAppUserRepository 
    {
        private readonly UserManager<AppUser> _userManager;
        public AppUserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser?> GetAppUserByIdAsync(string appUserId, bool trackChanges)
        {
            AppUser? appUser;

            if (trackChanges)
            {
                appUser = await _userManager.Users
                        .Include(appUser => appUser.Role)
                        .FirstOrDefaultAsync(appUser => appUser.Id == appUserId);
            }
            else
            {
                appUser = await _userManager.Users
                        .AsNoTracking()
                        .Include(appUser => appUser.Role)
                        .FirstOrDefaultAsync(appUser => appUser.Id == appUserId);
            }

            return appUser;
        }
    }
}
