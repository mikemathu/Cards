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

            builder.Property(status => status.StatusId).HasMaxLength(50);

            builder.Property(status => status.StatusId).ValueGeneratedOnAdd();

            builder.Property(status => status.Name).IsRequired().HasMaxLength(50);

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
                      StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo],
                      Name = StatusDetails.ToDo
                  },
                  new CardStatus
                  {
                      StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.InProgress],
                      Name = StatusDetails.InProgress
                  },
                    new CardStatus
                    {
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.Done],
                        Name = StatusDetails.Done
                    }
              );
        }
    }
}
