using Cards.Domain.Constants;
using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(status => status.StatusId);

            builder.Property(status => status.StatusId).HasColumnName("StatusId");

            builder.Property(status => status.StatusId).HasMaxLength(50);

            builder.Property(status => status.StatusId).ValueGeneratedOnAdd();

            builder.Property(status => status.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(appUser => appUser.Name).IsUnique();

            builder.HasMany<Card>()
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData
              (
                  new Status
                  {
                      StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo],
                      Name = StatusDetails.ToDo
                  },
                  new Status
                  {
                      StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.InProgress],
                      Name = StatusDetails.InProgress
                  },
                    new Status
                    {
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.Done],
                        Name = StatusDetails.Done
                    }
              );
        }
    }
}
