using Cards.Domain.Constants;

namespace Cards.Domain.Entities
{
    public class Status
    {
        public string StatusId { get; set; } = null!;
        public string Name { get; set; } = StatusDetails.ToDo;
    }
}
