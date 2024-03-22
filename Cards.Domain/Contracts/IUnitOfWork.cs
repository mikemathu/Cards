namespace Cards.Domain.Contracts
{
    public interface IUnitOfWork
    {
        public abstract Task SaveAsync();
    }
}
