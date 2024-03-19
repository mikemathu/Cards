using Cards.Domain.Constants;
using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.CodeDom.Compiler;

namespace Cards.Persistence.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> roleConfiguration)
        {

            roleConfiguration.Property(role => role.Id).HasColumnName("RoleId");

            roleConfiguration.Property(role => role.Id).HasMaxLength(50);

            roleConfiguration.Property(role => role.Name).IsRequired().HasMaxLength(50);

            roleConfiguration.HasIndex(role => role.Name).IsUnique();

            roleConfiguration.HasMany<AppUser>()
                  .WithOne(e => e.Role)
                  .HasForeignKey(e => e.RoleId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            roleConfiguration.HasData
                (
                    new Role
                    {
                        Id = RoleDetails.RoleNameToIdMappings[RoleDetails.Admin],
                        Name = RoleDetails.Admin
                    },
                    new Role
                    {
                        Id = RoleDetails.RoleNameToIdMappings[RoleDetails.Member],
                        Name = RoleDetails.Member
                    }
                );
        }
    }
}
