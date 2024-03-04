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

            builder.Property(status => status.Id).ValueGeneratedOnAdd();

            builder.Property(status => status.Name).IsRequired();

            builder.HasIndex(appUser => appUser.Name).IsUnique();

            builder.HasMany<Card>()
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
