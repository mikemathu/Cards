namespace Cards.Domain.Exceptions
{
    public class AppUserNotFoundException : NotFoundException
    {
        public AppUserNotFoundException(string appUserId)
            :base($"AppUser/Member with id {appUserId} doesn't exist in the database.")
        {
            
        }
    }
}
