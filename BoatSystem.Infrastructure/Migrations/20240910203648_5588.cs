using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _5588 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2b0d6e5-39a2-41ec-8389-4c5ea97f10b6", "AQAAAAIAAYagAAAAEO5krBL05A4QRZmEAY2JoSKs4KOObO5uc41Pqc4A8rqqFbDkjxc4T1W8cz8Zwr2Ivw==", "55b2cbd7-4545-4b1d-a26e-21a7a2233241" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e48c3b0a-7191-46a5-a90d-31ae173cddf6", "AQAAAAIAAYagAAAAEF8RUFm8Hq1/XxdVgi7lve9+CNar+Oo059n4HBgd0oIEm1lSmCG6PC3o3AIoAbRClA==", "73b2bc31-6b88-454b-ba86-2f08275acae2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d835eb5a-2865-4602-b941-0034cfce848d", "AQAAAAIAAYagAAAAEAwsWafCLBj9XOiGyrQWgN+yYuKcyuK27JXg82RCPXDuI9/QZjm0MBv9gzbFVc0a5A==", "ccf234b9-0e9b-4127-a021-3cc2e380af3c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36d1436b-649b-4c98-991f-dd82c4c94768", "AQAAAAIAAYagAAAAEI4wfwisprIIbS7ObZV3v3Yv/qnXWdha2IcOnBaHaxN/8Iwkoum0HTKYDQ9XrPARUw==", "f406ed82-5fd6-40ea-9672-52998121d5e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f53aaf5d-bdff-48f4-9945-bdcec3f157da", "AQAAAAIAAYagAAAAEGCWj9KWlvxsvFbI39wKrkpnClmQSt8Cl+WnTQV55/5fK7NH/1v4PCY/YsS0AM9wnw==", "f34fafd9-6638-4b63-903f-fa87dcc16e91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc9fca76-dda8-489c-9df6-b4bc54086b83", "AQAAAAIAAYagAAAAEJmQTVaAdE4T71UVvQiwCrjeyF6CduKYBhPe3V/wlNw72furE0hpnMhwWnGYIca6DQ==", "9f992b9b-79cb-4ee8-95a3-376b8e489d68" });
        }
    }
}
