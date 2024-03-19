using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updateseededdatatoincludenormalizednames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "99643f8b-ea99-48aa-b6c2-12235ea11a12", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEOsG5Zl6lTxrkhILjJiMBQiqUwXj8GN4XnSja8ACOlaS3Ap4T15AWlRftrcgQ/rYzA==", "063a8389-ecf2-42df-a7ee-01af431c7dcc", "admin@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "ben8c400-aa14-4fb9-868e-1202d25bff95",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9b0ad2b9-421e-471b-8ce2-cd7fae22e022", "BEN@GMAIL.COM", "BEN@GMAIL.COM", "AQAAAAIAAYagAAAAELbsbsuXX5Aj/cBMjB8ApD+WuK1Z43YRdM0k9x9BzyAL0eB3L/ufxglJCWhvUU1XHQ==", "303e045f-11fc-4a51-a368-36ae231c6750", "ben@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "6140b6c7-917d-4620-a22c-f62f3ccf5126", "ELI@GMAIL.COM", "ELI@GMAIL.COM", "AQAAAAIAAYagAAAAEG+jJFZXa2nofcyhwYWnwpUyIlFAIOKUXajXW4fF0dDMlIZBc8niTa+u5ecdHwNSFQ==", "7df7d330-a377-40f6-aa76-a7d22edf21e9", "elie@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "kev5f943-112f-4d49-888d-c671e210b8b8",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "59081216-c53a-42a2-a1e9-f1a960e18536", "KEV@GMAIL.COM", "KEV@GMAIL.COM", "AQAAAAIAAYagAAAAEDcwDt0XLgutdor5Th6DsubX3duZeVplAS6mB8X230oqttFNm1iz0k5xk+cIz6JPyw==", "789b1fbc-a19d-46a9-bd1c-a116ebce141b", "kev@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "39f9e3fd-2ff8-4c7e-b0b5-c0141414de53", "SAM@GMAIL.COM", "SAM@GMAIL.COM", "AQAAAAIAAYagAAAAEETUXSZPK1Px9LTaebFGmbq2RYpMVHXBWTYCDhmCi/M+sekUPGdbyW+pBYQIRyVCOQ==", "95c934e8-39c8-44e6-a7ad-73040eb239a3", "sam@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "suee8ebc-7959-4591-b86c-da19d3630419",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2df21b62-d365-4fcd-943e-a8c500a65fe7", "SUE@GMAIL.COM", "SUE@GMAIL.COM", "AQAAAAIAAYagAAAAEGRuW8rPYJgB7szrSHTNlgWePkPUq6HWpb1CDOkgTIQ0KtRAufOsTBIixS1dWVxTuQ==", "ee3c3ea4-5122-46a7-8992-e43a3e672a34", "sue@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "842d0d33-5555-4c18-b871-0dfb76e5ca46", null, null, "AQAAAAIAAYagAAAAECKxteV7I+hxq/qr/90e42nqzlneyDISf3onCtW5x6ZaJE8nxFt0I4FFOa/zNpGl5A==", "62d2f7e1-fb47-424b-b7ec-3dc0ca839f45", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "ben8c400-aa14-4fb9-868e-1202d25bff95",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "582df46d-f661-4234-93b3-1522a51a9fc9", null, null, "AQAAAAIAAYagAAAAEH8kLtLi/MpV/R47gNZiwIx2TDcrJMK8+psxmWb+Td8pnsD2Y7VBMMZ+ns/X9XTTbQ==", "eaa8b332-2e2c-40bc-8f4e-4b63941c26b2", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "df2fd2a5-e214-47b4-8db6-2c9399fce6f9", null, null, "AQAAAAIAAYagAAAAEKVYcHQ1Qfd+END7C9iJArKPvUpjTjtUeFbl5/ckAKFPfCrH1huMg5rlHac/bCgz8A==", "fdc979ba-fd4d-4bd2-9ae3-fcde97240adb", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "kev5f943-112f-4d49-888d-c671e210b8b8",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "d696173e-c9e6-4a13-ba24-cf4b8e85acf1", null, null, "AQAAAAIAAYagAAAAEMSt2T2eDIedgycUqPFqYRqIoH2eZ2XeegWYFdXp24jnCYXmiv7+AJ9cK1Y1Zv/fyg==", "ad3d9db9-ed10-4327-a77e-b500bf28f75d", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "26dc4482-4714-40bb-b63f-b84c1805107c", null, null, "AQAAAAIAAYagAAAAEIyjJE3L3JvmUtL/nV2oA/au9qcDlQrX9adikRxRS731Xy4GupU5nClNCG+W7Tk9BQ==", "6a9e0bcb-c75e-40e8-9f8f-3ed7fd9cbb5d", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "suee8ebc-7959-4591-b86c-da19d3630419",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "3a904fa3-2c26-4b1a-a921-0fc25b084c44", null, null, "AQAAAAIAAYagAAAAEOKCenc0USRwMThcv4lXFUfV1iS7klRXC0eL+pX734TEcWCwP2q+qVa3LuqLmuX7FQ==", "9e866649-8cd9-4371-8e93-d75633a17b06", null });

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
    }
}
