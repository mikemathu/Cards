namespace Cards.Domain.Exceptions
{
    public class CardDoesNotBelongToAppUserException : BadRequestException
    {
        public CardDoesNotBelongToAppUserException(string appUserId, string cardId)
            : base($"The card with the identifier '{cardId}' does not belong to the AppUser/Member with the identifier '{appUserId}' ")
        {            
        }
    }
}
