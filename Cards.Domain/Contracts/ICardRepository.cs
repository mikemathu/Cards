using Cards.Domain.Entities;
using Cards.Domain.Shared.RequestFeatures;

namespace Cards.Domain.Contracts
{
    public interface ICardRepository
    {
        public abstract Task<PagedList<Card>> GetAllCardsAsync(CardParameters cardParameters, 
            bool trackChanges, Dictionary<string, object> cardQueryFilters);
        public abstract Task<PagedList<Card>> GetCardsForUserAsync(string appUserId,CardParameters cardParameters,
            bool trackChanges, Dictionary<string, object> cardQueryFilters);
        public abstract Task<Card?> GetCardByIdAsync(string cardId, bool trackChanges);
        public abstract Task CreateCardAsync(Card card);
        public abstract void DeleteCard(Card card);         
    }
}
