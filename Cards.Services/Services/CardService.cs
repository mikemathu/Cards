using AutoMapper;
using Cards.Domain.Contracts;
using Cards.Domain.Entities;
using Cards.Domain.Exceptions;
using Cards.Domain.Shared.RequestFeatures;
using Cards.Services.Abstraction;
using Cards.Services.Dtos;

namespace Cards.Services.Services
{
    public class CardService(IAppUserRepository appUserRepository, ICardRepository cardRepository,
        IUnitOfWork unitOfWork, IMapper mapper) : ICardService
    {
        public async Task<(IEnumerable<CardDto> cards, MetaData metaData)> GetAllCardsAsync(string appUserId,
            CardParameters cardParameters, bool trackChanges)
        {  
            await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);

            Dictionary<string, object> cardQueryFilters = GetCardFiterParameters(cardParameters);

            PagedList<Card> cardsWithMetaData = await cardRepository.GetAllCardsAsync(
                    cardParameters, trackChanges, cardQueryFilters);               

            IEnumerable<CardDto> cardsDto = mapper.Map<IEnumerable<CardDto>>(cardsWithMetaData.Items);

            return (cards: cardsDto, metaData: cardsWithMetaData.MetaData);
        }

        public async Task<(IEnumerable<CardDto> cards, MetaData metaData)> GetCardsForUserAsync(string appUserId,
          CardParameters cardParameters, bool trackChanges)
        {
            await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);

            Dictionary<string, object> cardQueryFilters = GetCardFiterParameters(cardParameters);

            PagedList<Card> cardsWithMetaData = await cardRepository.GetCardsForUserAsync(
                    appUserId, cardParameters, trackChanges, cardQueryFilters);            

            IEnumerable<CardDto> cardsDto = mapper.Map<IEnumerable<CardDto>>(cardsWithMetaData.Items);

            return (cards: cardsDto, metaData: cardsWithMetaData.MetaData);
        }

        private static Dictionary<string, object> GetCardFiterParameters(CardParameters cardFilterParameters)
        {
            Dictionary<string, object> cardQueryFilters = [];

            if (cardFilterParameters.Name != "all")
                cardQueryFilters.Add("Name", cardFilterParameters.Name);

            if (cardFilterParameters.Color != "all")
                cardQueryFilters.Add("Color", cardFilterParameters.Color);

            if (cardFilterParameters.StatusId != "all")
                cardQueryFilters.Add("StatusId", cardFilterParameters.StatusId);

            if (cardFilterParameters.DateOfCreation != null)
                cardQueryFilters.Add("DateOfCreation", cardFilterParameters.DateOfCreation);

            return cardQueryFilters;
        }

        public async Task<CardDto> GetCardByIdAsync(string appUserId, string cardId, bool trackChanges)
        {
            AppUser appUserFromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);

            Card cardfromDb = await GetCardByIdAndCHeckIfItExistsAsync(cardId, trackChanges);

            if (cardfromDb.AppUserId != appUserFromDb.Id)
                throw new CardDoesNotBelongToAppUserException(appUserFromDb.Id, cardfromDb.CardId);

            CardDto cardDto = mapper.Map<CardDto>(cardfromDb);

            return cardDto;
        }

        public async Task<CardDto> CreateCardAsync(string appUserId, CardForCreationDto cardForCreationDto, bool trackChanges)
        {
            AppUser appUserFromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);

            Card cardEntity = mapper.Map<Card>(cardForCreationDto);

            cardEntity.AppUserId = appUserFromDb.Id;

            await cardRepository.CreateCardAsync(cardEntity);

            await unitOfWork.SaveAsync();

            Card cardfromDb = await GetCardByIdAndCHeckIfItExistsAsync(cardEntity.CardId, trackChanges);

            CardDto cardToReturn = mapper.Map<CardDto>(cardfromDb);

            return cardToReturn;
        }

        public async Task UpdateCardAsync(string appUserId, string cardId, CardForUpdateDto cardForUpdateDto,
            bool appUserTrackChanges, bool cardTrackChanges)
        {
            AppUser appUserFromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, appUserTrackChanges);

            Card cardfromDb = await GetCardByIdAndCHeckIfItExistsAsync(cardId, cardTrackChanges);

            if (cardfromDb.AppUserId != appUserFromDb.Id)
                throw new CardDoesNotBelongToAppUserException(appUserFromDb.Id, cardfromDb.CardId);

            mapper.Map(cardForUpdateDto, cardfromDb);

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteCardAsync(string appUserId, string cardId, bool trackChanges)
        {

            AppUser appUserFromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);

            Card cardfromDb = await GetCardByIdAndCHeckIfItExistsAsync(cardId, trackChanges);

            if (cardfromDb.AppUserId != appUserFromDb.Id)
                throw new CardDoesNotBelongToAppUserException(appUserFromDb.Id, cardfromDb.CardId);

            cardRepository.DeleteCard(cardfromDb);

            await unitOfWork.SaveAsync();
        }
        private async Task<AppUser> GetAppUserByIdAndCheckIfItExistsAsync(string appUserId, bool trackChanges)
        {
            AppUser? appUserfromDb = await appUserRepository.GetAppUserByIdAsync(appUserId, trackChanges)
                ?? throw new AppUserNotFoundException(appUserId);

            return appUserfromDb;
        }
        private async Task<Card> GetCardByIdAndCHeckIfItExistsAsync(string cardId, bool trackChanges)
        {
            Card? cardfromDb = await cardRepository.GetCardByIdAsync(cardId, trackChanges)
                ?? throw new CardNotFoundException(cardId);

            return cardfromDb;
        }   
    }
}