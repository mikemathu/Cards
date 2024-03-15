using System.ComponentModel.DataAnnotations;

namespace Cards.Services.Dtos
{
    public abstract class CardForManipulationDto
    {
        [Required(ErrorMessage = "Card name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Color { get; set; }
    }
}
