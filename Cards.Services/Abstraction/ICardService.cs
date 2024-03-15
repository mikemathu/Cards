using Cards.Domain.Shared.RequestFeatures;
using Cards.Services.Dtos;

namespace Cards.Services.Abstraction
{
    public interface ICardService
    {
        public abstract Task<(IEnumerable<CardDto> cards, MetaData metaData)>GetCardsAsync(
            int appUserId, CardParameters cardParameters, bool trackChanges);

        public abstract Task<CardDto> GetCardByIdAsync(int appUserId, int cardId, bool trackChanges);
        public abstract Task<CardDto> CreateCardAsync(int appUserId, CardForCreationDto cardForCreationDto, bool trackChanges);
        public abstract Task UpdateCardAsync(int appUserId, int cardId, CardForUpdateDto cardForUpdateDto, 
            bool appUserTrackChanges, bool cardTrackChanges);
        public abstract Task DeleteCardAsync(int appUserId, int cardId, bool trackChanges);
    }
}
