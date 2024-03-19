using Cards.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Cards.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string RoleId { get; set; } = RoleDetails.RoleNameToIdMappings[RoleDetails.Member];
        public Role Role { get; set; } = null!; //Required reference navigation to principal Role
        public ICollection<Card> Cards { get; } = new List<Card>(); //initializes the Cards property with an empty List<Card> when an instance of the AppUser entity is created. Hence Cards property is never null 
    }
}
