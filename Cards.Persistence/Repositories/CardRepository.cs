using Cards.Domain.Contracts;
using Cards.Domain.Entities;
using Cards.Domain.Shared.RequestFeatures;
using Cards.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cards.Persistence.Repositories
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(RepositoryDbContext repositoryDbContext)
            : base(repositoryDbContext)
        {
        }
        public async Task<PagedList<Card>> GetAllCardsAsync(CardParameters cardParameters, 
            bool trackChanges, Dictionary<string, object> cardQueryFilters)
        {
            PagedList<Card> pagedCards = await InitializeQueryWithTrackingPreference(trackChanges)
                .Include(card => card.CardStatus)
                .Include(card => card.AppUser)
                .FilterByCardQueryFilters(cardQueryFilters)
                .OrderBy(card => card.Name)
                .ToPagedListAsync(cardParameters.PageNumber, cardParameters.PageSize);//todo: add sorting

            return pagedCards;
        }
        public async Task<PagedList<Card>> GetCardsForUserAsync(string appUserId,CardParameters cardParameters,
           bool trackChanges, Dictionary<string, object> cardQueryFilters)
        {
            PagedList<Card> pagedCards = await InitializeQueryWithTrackingPreference(trackChanges)
           .Include(card => card.CardStatus)
           .Include(card => card.AppUser)
           .Where(card => card.AppUserId == appUserId)
           .FilterByCardQueryFilters(cardQueryFilters)
           .OrderBy(card => card.Name)
           .ToPagedListAsync(cardParameters.PageNumber, cardParameters.PageSize);//todo: add sorting

            return pagedCards;
        }
        public async Task<Card?> GetCardByIdAsync(string cardId, bool trackChanges)
        {
            return await GetByCondition(card => card.CardId.Equals(cardId), trackChanges)
                .Include(card => card.CardStatus)
                .Include(card => card.AppUser)
                .SingleOrDefaultAsync();
        }

        public async Task CreateCardAsync(Card card) => await CreateAsync(card);   

        public void DeleteCard(Card card) => Delete(card);

        public void DetatchCard(Card card) => Detach(card);
    }
}
