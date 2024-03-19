using AutoMapper;
using Cards.Domain.Constants;
using Cards.Domain.Contracts;
using Cards.Domain.Entities;
using Cards.Domain.Exceptions;
using Cards.Domain.Shared.RequestFeatures;
using Cards.Services.Abstraction;
using Cards.Services.Dtos;

namespace Cards.Services.Services
{
    public class CardService : ICardService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CardService(IAppUserRepository appUserRepository, ICardRepository cardRepository,  
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(IEnumerable<CardDto> cards, MetaData metaData)> GetAllCardsAsync(string appUserId,
            CardParameters cardParameters, bool trackChanges)
        {  
            AppUser appUserfromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);

            Dictionary<string, object> cardQueryFilters = GetCardFiterParameters(cardParameters);

            PagedList<Card> cardsWithMetaData = await _cardRepository.GetAllCardsAsync(
                    cardParameters, trackChanges, cardQueryFilters);               

            IEnumerable<CardDto> cardsDto = _mapper.Map<IEnumerable<CardDto>>(cardsWithMetaData.Items);

            return (cards: cardsDto, metaData: cardsWithMetaData.MetaData);
        }

        public async Task<(IEnumerable<CardDto> cards, MetaData metaData)> GetCardsForUserAsync(string appUserId,
          CardParameters cardParameters, bool trackChanges)
        {
            AppUser appUserfromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);

            Dictionary<string, object> cardQueryFilters = GetCardFiterParameters(cardParameters);

            PagedList<Card> cardsWithMetaData = await _cardRepository.GetCardsForUserAsync(
                    appUserId, cardParameters, trackChanges, cardQueryFilters);            

            IEnumerable<CardDto> cardsDto = _mapper.Map<IEnumerable<CardDto>>(cardsWithMetaData.Items);

            return (cards: cardsDto, metaData: cardsWithMetaData.MetaData);
        }

        private static Dictionary<string, object> GetCardFiterParameters(CardParameters cardFilterParameters)
        {
            Dictionary<string, object> cardQueryFilters = [];

            if (cardFilterParameters.Name != "all")
                cardQueryFilters.Add("Name", cardFilterParameters.Name);

            if (cardFilterParameters.Color != "all")
                cardQueryFilters.Add("Color", cardFilterParameters.Color);

            if (cardFilterParameters.StatusId != -1)
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

            CardDto cardDto = _mapper.Map<CardDto>(cardfromDb);

            return cardDto;
        }

        public async Task<CardDto> CreateCardAsync(string appUserId, CardForCreationDto cardForCreationDto, bool trackChanges)
        {
            AppUser appUserFromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);


            Card cardEntity = _mapper.Map<Card>(cardForCreationDto);


            cardEntity.AppUserId = appUserFromDb.Id;

            await _cardRepository.CreateCardAsync(cardEntity);

            await _unitOfWork.SaveAsync();

            _cardRepository.DetatchCard(cardEntity);

            Card cardfromDb = await GetCardByIdAndCHeckIfItExistsAsync(cardEntity.CardId, trackChanges);

            CardDto cardToReturn = _mapper.Map<CardDto>(cardfromDb);

            return cardToReturn;
        }

        public async Task UpdateCardAsync(string appUserId, string cardId, CardForUpdateDto cardForUpdateDto,
            bool appUserTrackChanges, bool cardTrackChanges)
        {
            AppUser appUserFromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, appUserTrackChanges);

            Card cardfromDb = await GetCardByIdAndCHeckIfItExistsAsync(cardId, cardTrackChanges);

            if (cardfromDb.AppUserId != appUserFromDb.Id)
                throw new CardDoesNotBelongToAppUserException(appUserFromDb.Id, cardfromDb.CardId);

            _mapper.Map(cardForUpdateDto, cardfromDb);

            await _unitOfWork.SaveAsync();

            _cardRepository.DetatchCard(cardfromDb);
        }

        public async Task DeleteCardAsync(string appUserId, string cardId, bool trackChanges)
        {

            AppUser appUserFromDb = await GetAppUserByIdAndCheckIfItExistsAsync(appUserId, trackChanges);

            Card cardfromDb = await GetCardByIdAndCHeckIfItExistsAsync(cardId, trackChanges);

            if (cardfromDb.AppUserId != appUserFromDb.Id)
                throw new CardDoesNotBelongToAppUserException(appUserFromDb.Id, cardfromDb.CardId);

            _cardRepository.DeleteCard(cardfromDb);

            await _unitOfWork.SaveAsync();
        }
        private async Task<AppUser> GetAppUserByIdAndCheckIfItExistsAsync(string appUserId, bool trackChanges)
        {
            AppUser? appUserfromDb = await _appUserRepository.GetAppUserByIdAsync(appUserId, trackChanges)
                ?? throw new AppUserNotFoundException(appUserId);

            return appUserfromDb;
        }
        private async Task<Card> GetCardByIdAndCHeckIfItExistsAsync(string cardId, bool trackChanges)
        {
            Card? cardfromDb = await _cardRepository.GetCardByIdAsync(cardId, trackChanges)
                ?? throw new CardNotFoundException(cardId);

            return cardfromDb;
        }   
    }
}