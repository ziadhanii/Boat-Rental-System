using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _00012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumPeople",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a032853-f039-4c14-820e-8ad7da2f2880", "AQAAAAIAAYagAAAAEP0s9ZQ6E7uTkAw4TcPl5eeNtbewmprOk9zFOUYR5EIc7Efi/2UPCuvMBZfH9lQv8g==", "837e4a5c-f826-46bd-b83a-13d49e000e6c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aab60d39-a0de-40fe-9988-c7d07f0ef24e", "AQAAAAIAAYagAAAAEPSpOx4jrkqKJCk+Ul65CbE+ueiOLI/9fpOguZKBkKSCLCmNXg7fBYrIYadhCjrd0A==", "2707d1fc-d2a2-4216-ba22-44e1f3d64fdc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a85123a3-aa42-4063-9bec-cbd2f883a515", "AQAAAAIAAYagAAAAEKp/goZsze4Dbjo2ERnPXCWAUb3BbRVweTdPFSTWOcO6kNSTWPLcT5Wxj5RYB9mA3A==", "170b5bcf-bac4-46a3-8ff3-ec8dfe64c57b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NumPeople",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1bd1531-6453-4f7c-930e-566e102abaab", "AQAAAAIAAYagAAAAED9VzrcNO+qJu2APmTVB2g15F3SM8qJXUU6RPtcTkc5YKbQmkN2iBmBMoMWOpmDfpQ==", "e26efb4e-7855-4b50-a9c7-bc9a27264d57" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9dd43db9-a2c6-473f-91bc-c7b0d3f0c919", "AQAAAAIAAYagAAAAELnKqDkAGr/vgFkfffTEZ0MtV9vG/KSh5T7F3Cx/l3VnTDF35+dXmpPkarfRacKQ5A==", "0bef12c6-8246-4380-8387-cc714783a491" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9eda4937-8cd8-4892-ade7-5e6391f75fa6", "AQAAAAIAAYagAAAAEPSt7UJJZbJkz3JkZvivS9AWWas09Epq0Ur+yqwPjRjPmOAPQd7rGLqlic6r53X0vQ==", "0387852a-54c3-430c-b670-662723f027d8" });
        }
    }
}
