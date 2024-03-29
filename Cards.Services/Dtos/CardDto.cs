﻿namespace Cards.Services.Dtos
{
    public class CardDto
    {
        public string CardId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; } = null!;
        public string CreatedByAppUser { get; set; } = null!;
        public string? Color { get; set; }
    }
}
