using System.ComponentModel.DataAnnotations;

namespace Cards.Services.Dtos
{
    public abstract class CardForManipulationDtoBase
    {
        [Required(ErrorMessage = "Card name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string Name { get; set; } = null!;

        [MaxLength(255, ErrorMessage = "Maximum length for the Description is 255 characters.")]
        public string? Description { get; set; }

        [MaxLength(7, ErrorMessage = "Maximum length for the Color is 7 characters.")]
        public string? Color { get; set; }
    }
}
