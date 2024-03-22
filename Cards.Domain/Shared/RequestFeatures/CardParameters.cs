using Cards.Domain.Constants;

namespace Cards.Domain.Shared.RequestFeatures
{
    public class CardParameters : RequestParameters
    {
        private string _status = null!;
        public CardParameters()
        {
            OrderBy = "Name";
        }
        public string Name { get; set; } = "all";
        public string Color { get; set; } = "all";
        public string StatusId { get; set; } = "all";
        public DateTime? DateOfCreation { get; set; } = null;

        public string? Status
        {
            get { return _status; }
            set
            {
                if(value != null)
                    _status = value.Trim();
                else
                    _status = null!;

                if (_status != null)
                {
                    StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;

                    if (_status.Equals(StatusDetails.ToDo, stringComparison))
                    {
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo];
                    }
                    else if (_status.Equals(StatusDetails.InProgress, stringComparison))
                    {
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.InProgress];
                    }
                    else if (_status.Equals(StatusDetails.Done, stringComparison))
                    {
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.Done]; ;
                    }
                }
            }
        }
    }
}
