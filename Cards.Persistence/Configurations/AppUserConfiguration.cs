using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(appUser => appUser.Id);

            builder.Property(appUser => appUser.Id).HasColumnName("AppUserId");

            builder.Property(appUser => appUser.Id).ValueGeneratedOnAdd();

            builder.Property(appUser => appUser.Email).IsRequired().HasMaxLength(60);

            builder.Property(appUser => appUser.Password).IsRequired().HasMaxLength(60);

            builder.HasIndex(appUser => appUser.Email).IsUnique();

            builder.HasMany(e => e.Cards)
                .WithOne(e => e.AppUser)
                .HasForeignKey(e => e.AppUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData
                (
                   new AppUser
                   {
                       Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                       Email = "john@gmail.com",
                       Password = "johnP@ssword",
                       RoleId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                   },
                    new AppUser
                    {
                        Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                        Email = "kev@gmail.com",
                        Password = "kevP@ssword",
                        RoleId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                    },
                    new AppUser
                    {
                        Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                        Email = "sue@gmail.com",
                        Password = "sueP@ssword",
                        RoleId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                    }
                );
        }
    }
}
