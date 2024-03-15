using System.ComponentModel.DataAnnotations;
using Cards.Domain.Constants;

namespace Cards.Domain.Entities
{
    public class Card//todo: attribute to addd length of each filed
    {
        private string? _color;
        public int CardId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
        public int StatusId { get; set; } = (int)StatusEnum.ToDo;
        public CardStatus CardStatus { get; set; } = null!;
        public int AppUserId { get; set; }
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
                        throw new ValidationException("Six alphanumeric characters are required for the color code.");
                    }
                }
                else
                {
                    throw new ArgumentException("The color code should start with '#'.");
                }
            }
        }
    }
    /* public class Card//todo: attribute to addd length of each filed
     {
         private string? _color;

         //[Column("CardId")]
         //[Key]
         public Guid Id { get; set; }

         //[Required(ErrorMessage = "Name is a required field.")]
         //[StringLength(100)]
         public string Name { get; set; } = string.Empty;//todo: make unique
         public string? Description { get; set; }
         public DateTime DateOfCreation { get; set; }
         public int StatusId { get; set; } //Required foreign key property
         public Status Status { get; set; } = null!; //Required reference navigation to principal Status
         public int AppUserId { get; set; } //Required foreign key property
         public AppUser AppUser { get; set; } = null!; //Required reference navigation to principal AppUser

         public string? Color
         {
             get { return _color; }
             set
             {
                 if (value != null && value != string.Empty)
                 {
                     if (value.StartsWith("#"))
                     {
                         if (value.ToArray().Skip(1).Count() == 6)
                         {
                             _color = value;
                         }
                         else
                         {
                             throw new ValidationException("Six alphanumeric characters are required for the color code.");//TODO: Confirm if its the right exception to throw
                         }
                     }
                     else
                     {
                         throw new ArgumentException("The color code should Start with #");//TODO: Confirm if its the right exception to throw
                     }
                 }
             }
         }
     }*/
}
