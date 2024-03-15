using Cards.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cards.Services.Dtos
{
    public class CardDto
    {
        public int CardId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int StatusId { get; set; }
        public int AppUserId { get; set; }
        public string? Color { get; set; }

    }
}
