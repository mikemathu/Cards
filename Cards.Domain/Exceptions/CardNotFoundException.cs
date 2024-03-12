using Cards.Domain.Entities;

namespace Cards.Domain.Exceptions
{
    public class CardNotFoundException : NotFoundException
    {
        public CardNotFoundException(int cardId)
            : base($"Card with id {cardId} doesn't exist in the database.")
        {
                
        }
    }
}
