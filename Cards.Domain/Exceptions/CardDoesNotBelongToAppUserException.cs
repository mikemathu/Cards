namespace Cards.Domain.Exceptions
{
    public class CardDoesNotBelongToAppUserException : BadRequestException
    {
        public CardDoesNotBelongToAppUserException(int appUserId, int cardId)
            : base($"The card with the identifier {cardId} does not belong to the AppUser/Member with the identifier {appUserId}")
        {
            
        }
    }
}
