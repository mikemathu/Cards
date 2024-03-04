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
            builder.HasKey(role => role.Id);

            builder.Property(role => role.Id).HasColumnName("RoleId");

            builder.Property(role => role.Id).ValueGeneratedOnAdd();

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
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Admin"
                    },
                    new Role
                    {
                        Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                        Name = "Member"
                    }
                );
        }
    }
}
