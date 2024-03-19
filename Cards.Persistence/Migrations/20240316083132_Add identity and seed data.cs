using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addidentityandseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "character varying(100)", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<string>(type: "character varying(100)", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "character varying(100)", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "character varying(100)", nullable: false),
                    RoleId = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "character varying(100)", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusId = table.Column<string>(type: "character varying(100)", nullable: false),
                    AppUserId = table.Column<string>(type: "character varying(100)", nullable: false),
                    Color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cards_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "RoleId", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "Adminf86-5601-41eb-a871-a660b2f0f449", null, "Role", "Admin", null },
                    { "Member8a-19f1-430e-aba5-9082dacfa9dd", null, "Role", "Member", null }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { "Done83ea-b4c1-4107-a66b-da86fcecf73f", "Done" },
                    { "InProgress643-4e2e-bba7-8ebebb32d606", "In Progress" },
                    { "Todo343d-f8ec-4197-b0b2-f3365f71f2e2", "ToDo" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "AppUserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin46d-9e9f-44d3-8425-263ba67509aa", 0, "4ce43708-3fba-4284-a349-9b8d45f03191", "admin@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEKmkkbKdQY9suPtXJHrWdSMI0mK9UKDVYAQYCL++ToOgW2n1fYK7Si+3piumX5KUgA==", null, false, "Adminf86-5601-41eb-a871-a660b2f0f449", "aae18be6-0f24-40ac-8ab2-021dee8d60aa", false, null },
                    { "ben8c400-aa14-4fb9-868e-1202d25bff95", 0, "02b8a7cf-29b2-4c78-87f6-8c8f57134603", "ben@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEKfCc9PwnhXMnPdUyRprlvm/vkSqH3P/FkZ75hOJswiiZByQEvnvT78BFS91Guf6ow==", null, false, "Member8a-19f1-430e-aba5-9082dacfa9dd", "74e6ed42-9cde-4eca-a725-c8405617f028", false, null },
                    { "elide650-28ea-4df1-bfc4-6b3e9a03d0de", 0, "162d235a-2f40-4ec7-a411-75a5031a5d8e", "elie@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEB1YGXAyy0sKztUhqEpTWEtdsubhALH1ZorwstEJJ/snaqG4fhFaitDzCa9EAI4p0w==", null, false, "Member8a-19f1-430e-aba5-9082dacfa9dd", "dd0d3138-cc54-4942-812f-d12613bad781", false, null },
                    { "kev5f943-112f-4d49-888d-c671e210b8b8", 0, "48d28b9c-d595-4616-ab03-17015f8127fd", "kev@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEMd84zxDbGLqRm9IJwpan4wNXJo/7AaUpZ012TW3eFf2qtEa7WjgneIaQwGcQ7FPaA==", null, false, "Member8a-19f1-430e-aba5-9082dacfa9dd", "8e5f8973-507e-497a-b4b1-b8e9ad86834a", false, null },
                    { "sam172c6-46e5-4b40-a0a7-54f2424c7791", 0, "7ba02dc4-7464-46b1-95d5-abfece6545b5", "sam@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEOjfrVQ64Myn/aQleLbluD6saiD159fUwHKKPXFEOgJHAzURr+QNzEDTbrSzM1BMtQ==", null, false, "Member8a-19f1-430e-aba5-9082dacfa9dd", "608dd3bc-a780-44ed-9014-045fe8e07dc8", false, null },
                    { "suee8ebc-7959-4591-b86c-da19d3630419", 0, "3b25b401-a269-4030-bc5e-689418c7917d", "sue@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAECMAU2AMY0kG0XcxI9chhMaS4/XjEfMAq0tgjpAIipmFFo3aT7hTarVr6H3R8tvc/g==", null, false, "Member8a-19f1-430e-aba5-9082dacfa9dd", "be39bb8d-cfbe-4370-9a3d-f2225601d85b", false, null }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "AppUserId", "Color", "DateOfCreation", "Description", "Name", "StatusId" },
                values: new object[,]
                {
                    { "clientMeeting-2f9e-4681-a499-4a2d1b2e36e4", "kev5f943-112f-4d49-888d-c671e210b8b8", "#800080", new DateTime(2024, 2, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Schedule a meeting with the client to discuss project updates.", "Client Meeting", "Todo343d-f8ec-4197-b0b2-f3365f71f2e2" },
                    { "clientMeeting-6c72-45fe-a7bf-4cd6d1d90c91", "suee8ebc-7959-4591-b86c-da19d3630419", "#8A2BE2", new DateTime(2024, 3, 5, 11, 0, 0, 0, DateTimeKind.Utc), "Review project scope and timeline with the client.", "Client Meeting", "Todo343d-f8ec-4197-b0b2-f3365f71f2e2" },
                    { "clientMeeting-7c8a-4a7d-9533-56a21b5c92e1", "suee8ebc-7959-4591-b86c-da19d3630419", "#4682B4", new DateTime(2024, 2, 25, 14, 30, 0, 0, DateTimeKind.Utc), "Discuss project milestones and deliverables with the client.", "Client Meeting", "InProgress643-4e2e-bba7-8ebebb32d606" },
                    { "fixbugs2-cd1d-43cd-b997-71a7f2a20096", "kev5f943-112f-4d49-888d-c671e210b8b8", "#ADD8E6", new DateTime(2024, 1, 20, 20, 37, 19, 0, DateTimeKind.Utc), "The system has bags to be fixed", "Fix bugs", "Todo343d-f8ec-4197-b0b2-f3365f71f2e2" },
                    { "systemInstallation-8fae-7488fc2c1b95", "kev5f943-112f-4d49-888d-c671e210b8b8", "#FF7F50", new DateTime(2024, 1, 15, 20, 37, 19, 0, DateTimeKind.Utc), "Installation of system to the new client.", "System Installation", "InProgress643-4e2e-bba7-8ebebb32d606" },
                    { "updateDatabase-9c1f-4e7d-8737-d1f4e1ef5933", "suee8ebc-7959-4591-b86c-da19d3630419", "#FFA500", new DateTime(2024, 2, 20, 10, 15, 0, 0, DateTimeKind.Utc), "Apply patches and optimize database performance.", "Update Database", "Todo343d-f8ec-4197-b0b2-f3365f71f2e2" },
                    { "updateDatabase-f8a1-49e2-b7ab-2f5c6d73c93d", "kev5f943-112f-4d49-888d-c671e210b8b8", "#32CD32", new DateTime(2024, 2, 5, 15, 20, 0, 0, DateTimeKind.Utc), "Perform necessary updates on the database.", "Update Database", "InProgress643-4e2e-bba7-8ebebb32d606" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_Name",
                table: "AspNetRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AppUserId",
                table: "Cards",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_StatusId",
                table: "Cards",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Name",
                table: "Status",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "AspNetRoles");
        }
    }
}
