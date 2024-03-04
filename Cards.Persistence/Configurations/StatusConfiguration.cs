using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(status => status.Id);

            builder.Property(status => status.Id).HasColumnName("StatusId");

            builder.Property(status => status.Id).ValueGeneratedOnAdd();

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
                      Id = new Guid("d9d4c053-49b6-410c-bc78-2d54a9991870"),
                      Name = "Todo"
                  },
                  new Status
                  {
                      Id = new Guid("d8d4c053-49b6-410c-bc78-2d54a9991870"),
                      Name = "In Progress"
                  },
                    new Status
                    {
                        Id = new Guid("d7d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Done"
                    }
              );
        }
    }
}
