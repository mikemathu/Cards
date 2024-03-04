using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(role => role.Id);

            builder.Property(role => role.Id).ValueGeneratedOnAdd();

            builder.Property(role => role.Name).IsRequired();

            builder.HasIndex(appUser => appUser.Name).IsUnique();

            builder.HasMany<AppUser>()
                  .WithOne(e => e.Role)
                  .HasForeignKey(e => e.RoleId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
