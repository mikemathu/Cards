using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.CodeDom.Compiler;

namespace Cards.Persistence.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(role => role.RoleId);

            builder.Property(role => role.RoleId).HasColumnName("RoleId");

            builder.Property(role => role.RoleId).ValueGeneratedOnAdd();

            builder.Property(role => role.Name).IsRequired().HasMaxLength(60);

            builder.HasIndex(appUser => appUser.Name).IsUnique();

            builder.HasMany<AppUser>()
                  .WithOne(e => e.Role)
                  .HasForeignKey(e => e.RoleId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasData
                (
                    new Role
                    {
                        RoleId = 1,
                        Name = "Admin"
                    },
                    new Role
                    {
                        RoleId = 2,
                        Name = "Member"
                    }
                );
        }
    }
}
