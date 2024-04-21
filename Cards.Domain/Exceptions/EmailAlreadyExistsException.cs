namespace Cards.Domain.Exceptions
{
    public sealed class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException() 
            : base("Email already exists.")
        {
        }
    }
}
