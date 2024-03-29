namespace Cards.Domain.Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;
        //private int _pagesize = 10;
        private int _pagesize = 2;

        public int PageNumber { get; set; } = 1;

        public int PageSize 
        {
            get => _pagesize;

            set => _pagesize = value > maxPageSize ? maxPageSize : value;
        }

        public string OrderBy { get; set; } = null!;
    }
}
