﻿namespace Cards.Domain.Exceptions
{
    public class CardNotFoundException : NotFoundException
    {
        public CardNotFoundException(string cardId)
            : base($"Card with id '{cardId}' doesn't exist in the database.")
        {                
        }
    }
}
