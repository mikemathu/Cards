using Cards.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> appUserConfiguration)
        {
            appUserConfiguration.Property(appUser => appUser.Id).HasColumnName("AppUserId");

            appUserConfiguration.Property(appUser => appUser.Id).HasMaxLength(50);

            appUserConfiguration.Property(appUser => appUser.PasswordHash).IsRequired().HasMaxLength(100);

            appUserConfiguration.Property(appUser => appUser.Email).IsRequired().HasMaxLength(30);


            appUserConfiguration.HasMany(e => e.Cards)
                .WithOne(e => e.AppUser)
                .HasForeignKey(e => e.AppUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            var hasher = new PasswordHasher<AppUser>();

            appUserConfiguration.HasData
                (
                   new AppUser
                   {
                       Id = "admin46d-9e9f-44d3-8425-263ba67509aa",
                       Email = "admin@gmail.com",
                       PasswordHash = hasher.HashPassword(null, "adminP@ssword1"),
                       RoleId = "Adminf86-5601-41eb-a871-a660b2f0f449"
                   },
                    new AppUser
                    {
                        Id = "kev5f943-112f-4d49-888d-c671e210b8b8",
                        Email = "kev@gmail.com",
                        PasswordHash = hasher.HashPassword(null, "kevP@ssword1"),
                        RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd"
                    },
                    new AppUser
                    {
                        Id = "suee8ebc-7959-4591-b86c-da19d3630419",
                        Email = "sue@gmail.com",
                        PasswordHash = hasher.HashPassword(null, "sueP@ssword1"),
                        RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd"
                    },
                    new AppUser
                    {
                        Id = "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                        Email = "sam@gmail.com",
                        PasswordHash = hasher.HashPassword(null, "samP@ssword1"),
                        RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd"
                    },
                    new AppUser
                    {
                        Id = "ben8c400-aa14-4fb9-868e-1202d25bff95",
                        Email = "ben@gmail.com",
                        PasswordHash = hasher.HashPassword(null, "benP@ssword1"),
                        RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd"
                    },
                    new AppUser
                    {
                        Id = "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                        Email = "elie@gmail.com",
                        PasswordHash = hasher.HashPassword(null, "eliP@ssword1"),
                        RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd"
                    }
                );
        }
    }
}
