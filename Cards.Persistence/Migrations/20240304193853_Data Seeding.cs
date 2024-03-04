using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Member" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { new Guid("d7d4c053-49b6-410c-bc78-2d54a9991870"), "Done" },
                    { new Guid("d8d4c053-49b6-410c-bc78-2d54a9991870"), "In Progress" },
                    { new Guid("d9d4c053-49b6-410c-bc78-2d54a9991870"), "Todo" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "AppUserId", "Email", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "sue@gmail.com", "sueP@ssword", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") },
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "john@gmail.com", "johnP@ssword", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "kev@gmail.com", "kevP@ssword", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "AppUserId", "Color", "DateOfCreation", "Description", "Name", "StatusId" },
                values: new object[,]
                {
                    { new Guid("c9d4c057-49b6-410c-bc78-2d54a9991870"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "#000000", new DateTime(2024, 1, 15, 20, 37, 19, 0, DateTimeKind.Utc), "Installation of system to the new client.", "System Installation", new Guid("d8d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("c9d4c059-49b6-410c-bc78-2d54a9991870"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "#000000", new DateTime(2024, 1, 20, 20, 37, 19, 0, DateTimeKind.Utc), "The system has bags to be fixed", "Fix bugs", new Guid("d9d4c053-49b6-410c-bc78-2d54a9991870") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "AppUserId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "AppUserId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: new Guid("c9d4c057-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: new Guid("c9d4c059-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: new Guid("d7d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "AppUserId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: new Guid("d8d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: new Guid("d9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));
        }
    }
}
