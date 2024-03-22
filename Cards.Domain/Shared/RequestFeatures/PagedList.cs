namespace Cards.Domain.Shared.RequestFeatures
{
  
    public class PagedList<T>
    {
        public List<T> Items { get; set; } = [];
        public MetaData MetaData { get; set; } = null!;
    }
}
