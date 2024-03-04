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

            builder.Property(card => card.Id).ValueGeneratedOnAdd();

            builder.Property(card => card.Name).IsRequired().HasMaxLength(100);

            builder.Property(card => card.Description).IsRequired(false).HasMaxLength(100);

            builder.Property(card => card.DateOfCreation).IsRequired();

            builder.Property(card => card.Color).IsRequired(false);

            builder.HasIndex(card => card.Name).IsUnique();
        }
    }
}
