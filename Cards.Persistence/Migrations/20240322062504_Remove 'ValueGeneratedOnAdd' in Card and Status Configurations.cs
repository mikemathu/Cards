using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveValueGeneratedOnAddinCardandStatusConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cards",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cards",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Cards",
                type: "character varying(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardId",
                table: "Cards",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "admin46d-9e9f-44d3-8425-263ba67509aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64cf844c-4854-4d85-a8cc-5dde1e0293c4", "AQAAAAIAAYagAAAAEDqLk2QidGa20vh/AEcJ1cHnc4Z7A6G2yQ/LO0NuAX1FBxZKIo/cFQjSA5mq5+eh6Q==", "d241ec4c-b764-4bf9-b41c-f0c709efaed7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "ben8c400-aa14-4fb9-868e-1202d25bff95",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d530089-7ebd-4c72-a646-d844dfa105a7", "AQAAAAIAAYagAAAAEFg2WZKjj1cqxLkUSzvzX0YMu18M5zuGK3ISAp5+eBOUTK9uTbLKYsXOcv+in+ylTQ==", "70a3513f-afae-4dea-92f4-25ee44fd5a13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7cae045-d203-4a48-b5ea-a4b4168be7c3", "AQAAAAIAAYagAAAAEBUIDhlYr22HjQmxaQcsb+Cjn/pvOJLX6X9+d/lk4jJxdU9ZBaVgmCh/ZQkOCDOy3Q==", "28529636-1162-43fe-b5ec-b462aca346c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "kev5f943-112f-4d49-888d-c671e210b8b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6844f7ab-0d72-4a5d-8a10-bad5206d5f02", "AQAAAAIAAYagAAAAENeI6w/qOWpxlzOdNHFWuMofUXaZ6TDgR12U/YXoTG9E2me6VgdBQYtYt0LEtQhH0g==", "c3181bd2-6f75-4ebe-9f63-63dd6b063338" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8b09a29-44e0-440b-81cb-0b1cac8e7088", "AQAAAAIAAYagAAAAEKd7NaXg36Ue4kt07Xo06ida34Zd4i3yK7hOnNR6QHUrcC/MGgY/p5Xtre9HkpntKg==", "ffe60348-93db-4d8e-9842-b4f9b26dee9f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "suee8ebc-7959-4591-b86c-da19d3630419",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9bcfc37-2887-49f4-ac43-501f0300dae8", "AQAAAAIAAYagAAAAEPtgwSqy2czM3kXMEHUr/quHfSVcVVoRbtjKi1sEi5JfSdEJaTQ2lQN3XF3Ojyp/hw==", "4f563bb6-e29d-4dd5-a42a-b04a8773c670" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: "clientMeeting-2f9e-4681-a499-4a2d1b2e36e4");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: "clientMeeting-6c72-45fe-a7bf-4cd6d1d90c91");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: "clientMeeting-7c8a-4a7d-9533-56a21b5c92e1");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: "fixbugs2-cd1d-43cd-b997-71a7f2a20096");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: "systemInstallation-8fae-7488fc2c1b95");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: "updateDatabase-9c1f-4e7d-8737-d1f4e1ef5933");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: "updateDatabase-f8a1-49e2-b7ab-2f5c6d73c93d");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cards",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Cards",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardId",
                table: "Cards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "admin46d-9e9f-44d3-8425-263ba67509aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f127bd5a-666e-41d4-bd3e-c2b80d735645", "AQAAAAIAAYagAAAAEAePdReFst0++ptiYB3+e5LQsAekQrqyQ4sFY7TpbAzCkQ/NTiwlzX7Uge252qOw6w==", "7a850996-98bc-477f-92d5-c37aa929f242" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "ben8c400-aa14-4fb9-868e-1202d25bff95",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "075992e2-6777-45f5-943c-113dd148d903", "AQAAAAIAAYagAAAAEHdxD2f+cargF0YBlnsj6S62lsXkRvxxOkr8vGRS8//EeS23YR3d6WMj0+U0VSjU2A==", "aa21b882-e17c-4e29-98d0-fee8d2c6fc08" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2377286-5ec3-405f-b816-08a1592bf69f", "AQAAAAIAAYagAAAAEEww/SpInE6WIGQ8LdEZfwbVZhY/yvGPKzkJ6GHOV7emfCXP2frpctNoNQjELwInkw==", "808634c4-3479-436b-b766-42a259d6952e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "kev5f943-112f-4d49-888d-c671e210b8b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c03ed726-c556-42f2-8f9f-7eea70a12ca3", "AQAAAAIAAYagAAAAEAMvv4j9lqEEUQa0lZPrb2r4n+vq3JTYG84ZjVLH/juE4gt7J+NKjz7WHTy7/MyZqQ==", "c15be853-9568-4d14-9b32-8149df658ae7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a43a81b-ad78-45fc-b1a2-a209dd77d82e", "AQAAAAIAAYagAAAAEGRu2GWXPXr6Rxl3f+ZrZeyZZhRfMQosGuLIyLg1RVbxrLvnqJB4X9JLFbVrMDBdKA==", "b1d77394-be51-49c1-836b-400d76e10fdf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "suee8ebc-7959-4591-b86c-da19d3630419",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d2e0819-8469-4dc1-b296-c06b7ba87018", "AQAAAAIAAYagAAAAEOXMNS592htiImnmHoxNFdb2+z2aJsd/SoO+PQYz5VeET7A/73Cc2y7qCWabfXsi0g==", "a399d6e3-53eb-4df4-b528-e96cb7004f1d" });
        }
    }
}
