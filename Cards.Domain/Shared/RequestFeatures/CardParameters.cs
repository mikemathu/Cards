namespace Cards.Domain.Shared.RequestFeatures
{
    public class CardParameters : RequestParameters
    {
        public CardParameters()
        {
            OrderBy = "Name";
        }
        public string Name { get; set; } = "all";
        public string Color { get; set; } = "all";
        public int StatusId { get; set; } = -1; //all
        public DateTime? DateOfCreation { get; set; } = null;      
    }
}
