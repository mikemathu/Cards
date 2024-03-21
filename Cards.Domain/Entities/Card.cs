using Cards.Domain.Constants;

namespace Cards.Domain.Entities
{
    public class Card//todo: attribute to addd length of each filed
    {
        private string? _color;
        public string CardId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
        public string StatusId { get; set; } = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo];
        public Status Status { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public string? Color
        {
            get { return _color; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _color = null; 
                    return;
                }

                if (value.StartsWith("#"))
                {
                    if (value.Length == 7) 
                    {
                        _color = value; 
                    }
                    else
                    {
                        throw new ArgumentException("Six alphanumeric characters are required for the color code.");
                    }
                }
                else
                {
                    throw new ArgumentException("The color code should start with '#'.");
                }
            }
        }
    }
}