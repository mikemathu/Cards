namespace Cards.Domain.Exceptions
{
    public class CreateUserFailedException : Exception
    {
        public CreateUserFailedException() 
            : base("Could not create user. Please try again.")
        {
        }
    }
}
