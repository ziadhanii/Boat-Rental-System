using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1148 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "314a5c91-52ed-467e-b332-2cdf2508f7a3", "AQAAAAIAAYagAAAAEOO+gArpMudx7Hge8RmkDCobXsI06en2lemYzHFyEJYTKsKIm3Ft3zmmHGeYMnrXPA==", "58e0ed3f-ab58-45fb-a7ed-b592c3c013e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0e7477b-83c1-4487-aaa5-83302450ff53", "AQAAAAIAAYagAAAAELsAtXZsarYQz0uCnx16ewJWhVZWBsYWJy4AOq15WU7U0CGQ/7JAj2CpFQ7TnuPU0A==", "e0820ba2-02d7-4bbd-8869-d1eb6c62f23d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63459b53-531b-48e1-8b82-979ed0a04654", "AQAAAAIAAYagAAAAELZmkz9OxtkvjhHURnh94vEEJ0v4Rxd2Q1yMZ/g1aY1bshf/O3TZQqQ718hncWwWNA==", "40b040b7-852d-447c-81a8-26b966498bb7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff9b49c8-1eaa-49fa-9b1f-51c9a3849b16", "AQAAAAIAAYagAAAAEG9/bzlXnvNWhgvz9XohlVYKYp95Ian/+D9d6vxDdJFMPaavsEBJsu/l83ORA3vZtg==", "d1860978-4a60-4446-ab4a-e6157ec630cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1f19a57-743a-4f78-9fe7-a8d1942e2299", "AQAAAAIAAYagAAAAEDv271lXIiQC9wIe5nOHVPPFOg0JKmp7fwu+4qgYOJSFL+jMIE95dorZduISgMfuug==", "920da688-dfc1-4d63-b847-f0e7305af2d9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8713cb53-17ea-4668-8851-b271ba90bbc6", "AQAAAAIAAYagAAAAEB8vneOW5LnvCM1hh2bhswW5fgyXw6X1xSYxtxnvCFRnXkvJ3BmBu+c7+RaaAWmwgg==", "b7ee868c-29af-4239-80d2-8cb887d8cad7" });
        }
    }
}
