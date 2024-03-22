using Cards.Domain.Constants;
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

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = appUserConfiguration.HasData
                (
                   new AppUser
                   {
                       Id = "admin46d-9e9f-44d3-8425-263ba67509aa",
                       UserName = "admin@gmail.com",
                       NormalizedUserName = "ADMIN@GMAIL.COM",
                       Email = "admin@gmail.com",
                       NormalizedEmail = "ADMIN@GMAIL.COM",
                       PasswordHash = hasher.HashPassword(null, "adminP@ssword1"),
                       RoleId = RoleDetails.RoleNameToIdMappings[RoleDetails.Admin]
                   },
                    new AppUser
                    {
                        Id = "kev5f943-112f-4d49-888d-c671e210b8b8",
                        UserName = "kev@gmail.com",
                        NormalizedUserName = "KEV@GMAIL.COM",
                        Email = "kev@gmail.com",
                        NormalizedEmail = "KEV@GMAIL.COM",
                        PasswordHash = hasher.HashPassword(null, "kevP@ssword1"),
                        RoleId = RoleDetails.RoleNameToIdMappings[RoleDetails.Member]
                    },
                    new AppUser
                    {
                        Id = "suee8ebc-7959-4591-b86c-da19d3630419",
                        UserName = "sue@gmail.com",
                        NormalizedUserName = "SUE@GMAIL.COM",
                        Email = "sue@gmail.com",
                        NormalizedEmail = "SUE@GMAIL.COM",
                        PasswordHash = hasher.HashPassword(null, "sueP@ssword1"),
                        RoleId = RoleDetails.RoleNameToIdMappings[RoleDetails.Member]
                    },
                    new AppUser
                    {
                        Id = "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                        UserName = "sam@gmail.com",
                        NormalizedUserName = "SAM@GMAIL.COM",
                        Email = "sam@gmail.com",
                        NormalizedEmail = "SAM@GMAIL.COM",
                        PasswordHash = hasher.HashPassword(null, password: "samP@ssword1"),
                        RoleId = RoleDetails.RoleNameToIdMappings[RoleDetails.Member]
                    },
                    new AppUser
                    {
                        Id = "ben8c400-aa14-4fb9-868e-1202d25bff95",
                        UserName = "ben@gmail.com",
                        NormalizedUserName = "BEN@GMAIL.COM",
                        Email = "ben@gmail.com",
                        NormalizedEmail = "BEN@GMAIL.COM",
                        PasswordHash = hasher.HashPassword(null, "benP@ssword1"),
                        RoleId = RoleDetails.RoleNameToIdMappings[RoleDetails.Member]
                    },
                    new AppUser
                    {
                        Id = "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                        UserName = "elie@gmail.com",
                        NormalizedUserName = "ELI@GMAIL.COM",
                        Email = "elie@gmail.com",
                        NormalizedEmail = "ELI@GMAIL.COM",
                        PasswordHash = hasher.HashPassword(null, "eliP@ssword1"),
                        RoleId = RoleDetails.RoleNameToIdMappings[RoleDetails.Member]
                    }
                );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
