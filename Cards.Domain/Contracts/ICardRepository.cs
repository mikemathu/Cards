﻿using Cards.Domain.Entities;
using Cards.Domain.Shared.RequestFeatures;

namespace Cards.Domain.Contracts
{
    public interface ICardRepository
    {
        public abstract Task<PagedList<Card>> GetAllCardsAsync(CardParameters cardParameters, 
            bool trackChanges, Dictionary<string, object> cardQueryFilters);
        public abstract Task<PagedList<Card>> GetCardsForUserAsync(int appUserId,CardParameters cardParameters,
            bool trackChanges, Dictionary<string, object> cardQueryFilters);
        public abstract Task<Card?> GetCardByIdAsync(int cardId, bool trackChanges);
        public abstract Task CreateCardAsync(Card card);
        public abstract void DeleteCard(Card card);        
        public abstract void DetatchCard(Card card);        
    }
}