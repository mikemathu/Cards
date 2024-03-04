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

            builder.Property(appUser => appUser.Id).ValueGeneratedOnAdd();

            builder.Property(appUser => appUser.Email).IsRequired();

            builder.Property(appUser => appUser.Password).IsRequired();

            builder.HasIndex(appUser => appUser.Email).IsUnique();

            builder.HasMany(e => e.Cards)
                .WithOne(e => e.AppUser)
                .HasForeignKey(e => e.AppUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


       /*     builder.HasData
                (
                    new AppUser
                    {

                    },
                    new AppUser
                    {

                    },
                     new AppUser
                     {

                     },
                    new AppUser
                    {

                    }
                );*/

        }
    }
}
