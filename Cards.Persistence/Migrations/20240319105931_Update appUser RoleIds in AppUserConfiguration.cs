using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateappUserRoleIdsinAppUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "admin46d-9e9f-44d3-8425-263ba67509aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07629d44-002f-438c-9749-1a8ba43384c9", "AQAAAAIAAYagAAAAEG2B5aItkRZsCrX6dPsI2Gupp7cOfjiPsS8TKkLvqK3X5zeCBy4DtBP7OPTI8D+ARg==", "ed28a54d-d61c-436b-9ab9-7911e9f5cfa7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "ben8c400-aa14-4fb9-868e-1202d25bff95",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f235d9ba-66a8-4a8e-ad5f-b76d896eb8fa", "AQAAAAIAAYagAAAAECt9/lXUuRDU12ME0E8yQpM9vvC+4T23fDGFdoALCkQuA/WOLWMp7egxBRTRYVe6hw==", "b0c79cc0-30e6-4112-83bb-2f342d37d5a1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "elide650-28ea-4df1-bfc4-6b3e9a03d0de",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "483a07c7-6177-4fb1-a4bc-a377a4816368", "AQAAAAIAAYagAAAAEHw6+LGsifDKeuUvgpjWzq1KVvZv3QUdb37lCdY3rb/HvuRgVYWg77hHjFZw2zupMQ==", "85603385-520e-4755-8f72-08df09791154" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "kev5f943-112f-4d49-888d-c671e210b8b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e9ac709-ed9c-4d4f-a790-309bb35c6218", "AQAAAAIAAYagAAAAENn8EfyLLqOy3JQJWYfDa1COVr7urX54AyPAl5v0PtMXvVE6Nw4nuE//gzk7xT6+Lw==", "65d94872-f227-48e2-b20c-80565e351047" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "sam172c6-46e5-4b40-a0a7-54f2424c7791",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d668e44c-a1e3-4100-9594-9333ef353976", "AQAAAAIAAYagAAAAEL/uEuwDyT71tM/eqjAnmoRBnx4kwPNiE5c8sB/6jH2ttztNRKL/GDb1sKGM331pUQ==", "73323f0a-c5de-4fa2-9a24-31a58e8ce91e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "AppUserId",
                keyValue: "suee8ebc-7959-4591-b86c-da19d3630419",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "785fe202-a7a2-43a8-92e1-a624268a9065", "AQAAAAIAAYagAAAAENtx0MKVM7U9ZALXYRbxqOB8ieunPEBanZ6hSN30UqwwWiM0V/XRPxkBEl8R6LQz7A==", "fe69d17b-8b9c-4a82-864d-5d77201d35dd" });
        }
    }
}
