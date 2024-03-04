using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(card => card.Id);

            builder.Property(card => card.Id).HasColumnName("CardId");

            builder.Property(card => card.Id).ValueGeneratedOnAdd();

            builder.Property(card => card.Name).IsRequired().HasMaxLength(60);

            builder.Property(card => card.Description).IsRequired(false).HasMaxLength(255);

            builder.Property(card => card.DateOfCreation).IsRequired();

            builder.Property(card => card.Color).IsRequired(false).HasMaxLength(255);

            builder.HasIndex(card => card.Name).IsUnique();

            builder.HasData
                (
                    new Card
                    {
                        Id = new Guid("c9d4c059-49b6-410c-bc78-2d54a9991870"),
                        Name = "Fix bugs",
                        Description = "The system has bags to be fixed",
                        DateOfCreation = new DateTime(2024, 1, 20, 20, 37, 19, 0, DateTimeKind.Utc),
                        StatusId = new Guid("d9d4c053-49b6-410c-bc78-2d54a9991870"),
                        AppUserId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                        Color = "#000000"
                    },
                    new Card
                    {
                        Id = new Guid("c9d4c057-49b6-410c-bc78-2d54a9991870"),
                        Name = "System Installation",
                        Description = "Installation of system to the new client.",
                        DateOfCreation = new DateTime(2024, 1, 15, 20, 37, 19, 0, DateTimeKind.Utc),
                        StatusId = new Guid("d8d4c053-49b6-410c-bc78-2d54a9991870"),
                        AppUserId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                        Color = "#000000"
                    }
                );
        }
    }
}
