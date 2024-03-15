using Cards.Domain.Constants;
using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(card => card.CardId);

            builder.Property(card => card.CardId).HasColumnName("CardId");

            builder.Property(card => card.CardId).ValueGeneratedOnAdd();

            builder.Property(card => card.Name).IsRequired().HasMaxLength(60);

            builder.Property(card => card.Description).IsRequired(false).HasMaxLength(255);

            builder.Property(card => card.DateOfCreation).IsRequired();

            builder.Property(card => card.Color).IsRequired(false).HasMaxLength(255);

            //builder.HasIndex(card => card.Name).IsUnique();

            builder.HasData
                (
                    new Card
                    {
                        CardId = 1,
                        Name = "Fix bugs",
                        Description = "The system has bags to be fixed",
                        DateOfCreation = new DateTime(2024, 1, 20, 20, 37, 19, 0, DateTimeKind.Utc),
                        StatusId = (int)StatusEnum.ToDo,
                        AppUserId = 2,
                        Color = "#000000"
                    },
                    new Card
                    {
                        CardId = 2,
                        Name = "System Installation",
                        Description = "Installation of system to the new client.",
                        DateOfCreation = new DateTime(2024, 1, 15, 20, 37, 19, 0, DateTimeKind.Utc),
                        StatusId = (int)StatusEnum.ToDo,
                        AppUserId = 2,
                        Color = "#000000"
                    }
                );
        }
    }
}
