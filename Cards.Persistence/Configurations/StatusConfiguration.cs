using Cards.Domain.Constants;
using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class StatusConfiguration : IEntityTypeConfiguration<CardStatus>
    {
        public void Configure(EntityTypeBuilder<CardStatus> builder)
        {
            builder.HasKey(status => status.StatusId);

            builder.Property(status => status.StatusId).HasColumnName("StatusId");

            builder.Property(status => status.StatusId).ValueGeneratedOnAdd();

            builder.Property(status => status.Name).IsRequired().HasMaxLength(60);

            builder.HasIndex(appUser => appUser.Name).IsUnique();

            builder.HasMany<Card>()
                .WithOne(e => e.CardStatus)
                .HasForeignKey(e => e.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData
              (
                  new CardStatus
                  {
                      StatusId = 1,
                      Name = StatusName.ToDo
                  },
                  new CardStatus
                  {
                      StatusId = 2,
                      Name = StatusName.InProgress
                  },
                    new CardStatus
                    {
                        StatusId = 3,
                        Name = StatusName.Done
                    }
              );
        }
    }
}
