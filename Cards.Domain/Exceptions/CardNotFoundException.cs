namespace Cards.Domain.Exceptions
{
    public sealed class CardNotFoundException : NotFoundException
    {
        public CardNotFoundException(string cardId)
            : base($"Card with id '{cardId}' doesn't exist in the database.")
        {                
        }
    }
}
