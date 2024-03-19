using Cards.Domain.Entities;
using Cards.Domain.Shared.RequestFeatures;
using Cards.Services.Dtos;

namespace Cards.Services.Abstraction
{
    public interface ICardService
    {
        public abstract Task<(IEnumerable<CardDto> cards, MetaData metaData)>GetAllCardsAsync(
            string appUserId, CardParameters cardParameters, bool trackChanges);
        public abstract Task<(IEnumerable<CardDto> cards, MetaData metaData)>GetCardsForUserAsync(
            string appUserId, CardParameters cardParameters, bool trackChanges);

        public abstract Task<CardDto> GetCardByIdAsync(string appUserId, string cardId, bool trackChanges);
        public abstract Task<CardDto> CreateCardAsync(string appUserId, CardForCreationDto cardForCreationDto, bool trackChanges);
        public abstract Task UpdateCardAsync(string appUserId, string cardId, CardForUpdateDto cardForUpdateDto, 
            bool appUserTrackChanges, bool cardTrackChanges);
        public abstract Task DeleteCardAsync(string appUserId, string cardId, bool trackChanges);
    }
}
