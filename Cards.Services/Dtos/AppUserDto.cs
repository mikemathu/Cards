using Cards.Domain.Entities;

namespace Cards.Services.Dtos
{
    public class AppUserDto
    {
        public int AppUserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } 
        //public Role Role { get; set; } = null!; 
        //public ICollection<Card> Cards { get; } = new List<Card>();
    }
}
