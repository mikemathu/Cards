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
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        public bool IsDateOfCreationValid => EndDate > StartDate;        
    }
}
