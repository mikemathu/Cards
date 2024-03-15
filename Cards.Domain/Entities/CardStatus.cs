using Cards.Domain.Constants;

namespace Cards.Domain.Entities
{
    public class CardStatus
    {
        public int StatusId { get; set; }
        public string Name { get; set; } = StatusName.ToDo;
    }
}
