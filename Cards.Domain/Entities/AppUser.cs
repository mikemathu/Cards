using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cards.Domain.Entities
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
        public Guid RoleId { get; set; } //Required foreign key property
        public Role Role { get; set; } = null!; //Required reference navigation to principal Role
        public ICollection<Card> Cards { get; } = new List<Card>(); //initializes the Cards property with an empty List<Card> when an instance of the AppUser entity is created. Hence Cards property is never null 
    }


/*    public class AppUser
    {
        //[Column("AppUserId")]
        //[Key]
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Email is a required field.")]
        //[EmailAddress]
        public string Email { get; set; } = string.Empty; //tODO: make email unique 

        //[Required(ErrorMessage = "Password is a required field.")]
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } //Required foreign key property
        public Role Role { get; set; } = null!; //Required reference navigation to principal Role
        public ICollection<Card> Cards { get; } = new List<Card>(); //initializes the Cards property with an empty List<Card> when an instance of the AppUser entity is created. Hence Cards property is never null 
    }*/
}
