using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Changefieldsmaximumlength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Status",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "Status",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "Cards",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cards",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Cards",
                type: "character varying(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Cards",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "CardId",
                table: "Cards",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "admin46d-9e9f-44d3-8425-263ba67509aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "842d0d33-5555-4c18-b871-0dfb76e5ca46", "AQAAAAIAAYagAAAAECKxteV7I+hxq/qr/90e42nqzlneyDISf3onCtW5x6ZaJE8nxFt0I4FFOa/zNpGl5A==", "62d2f7e1-fb47-424b-b7ec-3dc0ca839f45" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "ben8c400-aa14-4fb9-868e-1202d25bff95",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "582df46d-f661-4234-93b3-1522a51a9fc9", "AQAAAAIAAYagAAAAEH8kLtLi/MpV/R47gNZiwIx2TDcrJMK8+psxmWb+Td8pnsD2Y7VBMMZ+ns/X9XTTbQ==", "eaa8b332-2e2c-40bc-8f4e-4b63941c26b2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df2fd2a5-e214-47b4-8db6-2c9399fce6f9", "AQAAAAIAAYagAAAAEKVYcHQ1Qfd+END7C9iJArKPvUpjTjtUeFbl5/ckAKFPfCrH1huMg5rlHac/bCgz8A==", "fdc979ba-fd4d-4bd2-9ae3-fcde97240adb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "kev5f943-112f-4d49-888d-c671e210b8b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d696173e-c9e6-4a13-ba24-cf4b8e85acf1", "AQAAAAIAAYagAAAAEMSt2T2eDIedgycUqPFqYRqIoH2eZ2XeegWYFdXp24jnCYXmiv7+AJ9cK1Y1Zv/fyg==", "ad3d9db9-ed10-4327-a77e-b500bf28f75d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26dc4482-4714-40bb-b63f-b84c1805107c", "AQAAAAIAAYagAAAAEIyjJE3L3JvmUtL/nV2oA/au9qcDlQrX9adikRxRS731Xy4GupU5nClNCG+W7Tk9BQ==", "6a9e0bcb-c75e-40e8-9f8f-3ed7fd9cbb5d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "suee8ebc-7959-4591-b86c-da19d3630419",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a904fa3-2c26-4b1a-a921-0fc25b084c44", "AQAAAAIAAYagAAAAEOKCenc0USRwMThcv4lXFUfV1iS7klRXC0eL+pX734TEcWCwP2q+qVa3LuqLmuX7FQ==", "9e866649-8cd9-4371-8e93-d75633a17b06" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Status",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "Status",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "Cards",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cards",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Cards",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Cards",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "CardId",
                table: "Cards",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "admin46d-9e9f-44d3-8425-263ba67509aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ce43708-3fba-4284-a349-9b8d45f03191", "AQAAAAIAAYagAAAAEKmkkbKdQY9suPtXJHrWdSMI0mK9UKDVYAQYCL++ToOgW2n1fYK7Si+3piumX5KUgA==", "aae18be6-0f24-40ac-8ab2-021dee8d60aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "ben8c400-aa14-4fb9-868e-1202d25bff95",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02b8a7cf-29b2-4c78-87f6-8c8f57134603", "AQAAAAIAAYagAAAAEKfCc9PwnhXMnPdUyRprlvm/vkSqH3P/FkZ75hOJswiiZByQEvnvT78BFS91Guf6ow==", "74e6ed42-9cde-4eca-a725-c8405617f028" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "162d235a-2f40-4ec7-a411-75a5031a5d8e", "AQAAAAIAAYagAAAAEB1YGXAyy0sKztUhqEpTWEtdsubhALH1ZorwstEJJ/snaqG4fhFaitDzCa9EAI4p0w==", "dd0d3138-cc54-4942-812f-d12613bad781" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "kev5f943-112f-4d49-888d-c671e210b8b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48d28b9c-d595-4616-ab03-17015f8127fd", "AQAAAAIAAYagAAAAEMd84zxDbGLqRm9IJwpan4wNXJo/7AaUpZ012TW3eFf2qtEa7WjgneIaQwGcQ7FPaA==", "8e5f8973-507e-497a-b4b1-b8e9ad86834a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ba02dc4-7464-46b1-95d5-abfece6545b5", "AQAAAAIAAYagAAAAEOjfrVQ64Myn/aQleLbluD6saiD159fUwHKKPXFEOgJHAzURr+QNzEDTbrSzM1BMtQ==", "608dd3bc-a780-44ed-9014-045fe8e07dc8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "suee8ebc-7959-4591-b86c-da19d3630419",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b25b401-a269-4030-bc5e-689418c7917d", "AQAAAAIAAYagAAAAECMAU2AMY0kG0XcxI9chhMaS4/XjEfMAq0tgjpAIipmFFo3aT7hTarVr6H3R8tvc/g==", "be39bb8d-cfbe-4370-9a3d-f2225601d85b" });
        }
    }
}
