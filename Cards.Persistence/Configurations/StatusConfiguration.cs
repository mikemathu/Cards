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

            builder.Property(status => status.StatusId).ValueGeneratedOnAdd();

            builder.Property(status => status.Name).IsRequired().HasMaxLength(60);

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
                      StatusId = 1,
                      Name = "Todo"
                  },
                  new Status
                  {
                      StatusId = 2,
                      Name = "In Progress"
                  },
                    new Status
                    {
                        StatusId = 3,
                        Name = "Done"
                    }
              );
        }
    }
}
