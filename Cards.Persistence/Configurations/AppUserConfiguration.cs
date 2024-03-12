using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> appUserConfiguration)
        {
            appUserConfiguration.HasKey(appUser => appUser.AppUserId);

            appUserConfiguration.Property(appUser => appUser.AppUserId).HasColumnName("AppUserId");

            appUserConfiguration.Property(appUser => appUser.AppUserId).ValueGeneratedOnAdd();

            appUserConfiguration.Property(appUser => appUser.Email).IsRequired().HasMaxLength(60);

            appUserConfiguration.Property(appUser => appUser.Password).IsRequired().HasMaxLength(60);

            appUserConfiguration.HasIndex(appUser => appUser.Email).IsUnique();

            appUserConfiguration.HasMany(e => e.Cards)
                .WithOne(e => e.AppUser)
                .HasForeignKey(e => e.AppUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            appUserConfiguration.HasData
                (
                   new AppUser
                   {
                       AppUserId = 1,
                       Email = "john@gmail.com",
                       Password = "johnP@ssword",
                       RoleId = 1
                   },
                    new AppUser
                    {
                        AppUserId = 2,
                        Email = "kev@gmail.com",
                        Password = "kevP@ssword",
                        RoleId = 2
                    },
                    new AppUser
                    {
                        AppUserId = 3,
                        Email = "sue@gmail.com",
                        Password = "sueP@ssword",
                        RoleId = 2
                    }
                );
        }
    }
}
