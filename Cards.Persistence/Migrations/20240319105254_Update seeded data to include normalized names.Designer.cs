﻿// <auto-generated />
using System;
using Cards.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cards.Persistence.Migrations
{
    [DbContext(typeof(RepositoryDbContext))]
    [Migration("20240319105254_Update seeded data to include normalized names")]
    partial class Updateseededdatatoincludenormalizednames
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cards.Domain.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("AppUserId");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("character varying(50)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "admin46d-9e9f-44d3-8425-263ba67509aa",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "99643f8b-ea99-48aa-b6c2-12235ea11a12",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEOsG5Zl6lTxrkhILjJiMBQiqUwXj8GN4XnSja8ACOlaS3Ap4T15AWlRftrcgQ/rYzA==",
                            PhoneNumberConfirmed = false,
                            RoleId = "Adminf86-5601-41eb-a871-a660b2f0f449",
                            SecurityStamp = "063a8389-ecf2-42df-a7ee-01af431c7dcc",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com"
                        },
                        new
                        {
                            Id = "kev5f943-112f-4d49-888d-c671e210b8b8",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "59081216-c53a-42a2-a1e9-f1a960e18536",
                            Email = "kev@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "KEV@GMAIL.COM",
                            NormalizedUserName = "KEV@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEDcwDt0XLgutdor5Th6DsubX3duZeVplAS6mB8X230oqttFNm1iz0k5xk+cIz6JPyw==",
                            PhoneNumberConfirmed = false,
                            RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd",
                            SecurityStamp = "789b1fbc-a19d-46a9-bd1c-a116ebce141b",
                            TwoFactorEnabled = false,
                            UserName = "kev@gmail.com"
                        },
                        new
                        {
                            Id = "suee8ebc-7959-4591-b86c-da19d3630419",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2df21b62-d365-4fcd-943e-a8c500a65fe7",
                            Email = "sue@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "SUE@GMAIL.COM",
                            NormalizedUserName = "SUE@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEGRuW8rPYJgB7szrSHTNlgWePkPUq6HWpb1CDOkgTIQ0KtRAufOsTBIixS1dWVxTuQ==",
                            PhoneNumberConfirmed = false,
                            RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd",
                            SecurityStamp = "ee3c3ea4-5122-46a7-8992-e43a3e672a34",
                            TwoFactorEnabled = false,
                            UserName = "sue@gmail.com"
                        },
                        new
                        {
                            Id = "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "39f9e3fd-2ff8-4c7e-b0b5-c0141414de53",
                            Email = "sam@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "SAM@GMAIL.COM",
                            NormalizedUserName = "SAM@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEETUXSZPK1Px9LTaebFGmbq2RYpMVHXBWTYCDhmCi/M+sekUPGdbyW+pBYQIRyVCOQ==",
                            PhoneNumberConfirmed = false,
                            RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd",
                            SecurityStamp = "95c934e8-39c8-44e6-a7ad-73040eb239a3",
                            TwoFactorEnabled = false,
                            UserName = "sam@gmail.com"
                        },
                        new
                        {
                            Id = "ben8c400-aa14-4fb9-868e-1202d25bff95",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9b0ad2b9-421e-471b-8ce2-cd7fae22e022",
                            Email = "ben@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "BEN@GMAIL.COM",
                            NormalizedUserName = "BEN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAELbsbsuXX5Aj/cBMjB8ApD+WuK1Z43YRdM0k9x9BzyAL0eB3L/ufxglJCWhvUU1XHQ==",
                            PhoneNumberConfirmed = false,
                            RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd",
                            SecurityStamp = "303e045f-11fc-4a51-a368-36ae231c6750",
                            TwoFactorEnabled = false,
                            UserName = "ben@gmail.com"
                        },
                        new
                        {
                            Id = "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6140b6c7-917d-4620-a22c-f62f3ccf5126",
                            Email = "elie@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ELI@GMAIL.COM",
                            NormalizedUserName = "ELI@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEG+jJFZXa2nofcyhwYWnwpUyIlFAIOKUXajXW4fF0dDMlIZBc8niTa+u5ecdHwNSFQ==",
                            PhoneNumberConfirmed = false,
                            RoleId = "Member8a-19f1-430e-aba5-9082dacfa9dd",
                            SecurityStamp = "7df7d330-a377-40f6-aa76-a7d22edf21e9",
                            TwoFactorEnabled = false,
                            UserName = "elie@gmail.com"
                        });
                });

            modelBuilder.Entity("Cards.Domain.Entities.Card", b =>
                {
                    b.Property<string>("CardId")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StatusId")
                        .IsRequired()
                        .HasColumnType("character varying(50)");

                    b.HasKey("CardId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("StatusId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Cards.Domain.Entities.CardStatus", b =>
                {
                    b.Property<string>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("StatusId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("StatusId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            StatusId = "Todo343d-f8ec-4197-b0b2-f3365f71f2e2",
                            Name = "ToDo"
                        },
                        new
                        {
                            StatusId = "InProgress643-4e2e-bba7-8ebebb32d606",
                            Name = "In Progress"
                        },
                        new
                        {
                            StatusId = "Done83ea-b4c1-4107-a66b-da86fcecf73f",
                            Name = "Done"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("RoleId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("character varying(50)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("character varying(50)");

                    b.Property<string>("RoleId")
                        .HasColumnType("character varying(50)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Cards.Domain.Entities.Role", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Role");

                    b.HasData(
                        new
                        {
                            Id = "Adminf86-5601-41eb-a871-a660b2f0f449",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = "Member8a-19f1-430e-aba5-9082dacfa9dd",
                            Name = "Member"
                        });
                });

            modelBuilder.Entity("Cards.Domain.Entities.AppUser", b =>
                {
                    b.HasOne("Cards.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Cards.Domain.Entities.Card", b =>
                {
                    b.HasOne("Cards.Domain.Entities.AppUser", "AppUser")
                        .WithMany("Cards")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Cards.Domain.Entities.CardStatus", "CardStatus")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("CardStatus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Cards.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Cards.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cards.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Cards.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cards.Domain.Entities.AppUser", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
