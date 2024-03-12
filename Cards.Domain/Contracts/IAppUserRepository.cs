using Cards.Domain.Entities;

namespace Cards.Domain.Contracts
{
    public interface IAppUserRepository
    {
        public abstract Task<AppUser?> GetAppUserByIdAsync(int appUserId, bool trackChanges);
    }
}
