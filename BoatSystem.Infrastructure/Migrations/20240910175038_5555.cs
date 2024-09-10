using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _5555 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeople",
                table: "BoatBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPeople",
                table: "BoatBookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a215b737-ec00-46a7-aa85-3c53137032a1", "AQAAAAIAAYagAAAAEGKdGv+m3SgKzHaejKC+xaOY8DGcfnp6viTlUvwGR1FEbv46V0gaMlzt3NqMP6MCYw==", "3a440f44-a4ca-4bff-99dd-548a33c85520" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2014461a-9356-4c90-b8d0-d57cdb456dc1", "AQAAAAIAAYagAAAAEC6cOK07t6l5VH636fjnMU23PF7ZmyRT3aM7eqkLdbmgHYBNIFFEcWB/20EHdjHJQw==", "0fcd0da6-4d5b-4636-9923-1d97d31adb05" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ddf2378-fd82-4794-a7cd-441768cb7c63", "AQAAAAIAAYagAAAAEGk3DcuRFNFWikt6bdlL8o8MXFvyhYpAtHzoBfwHsxjlR2cMqCIyUSnqj7dhhhI8hA==", "782ee12a-2817-497e-a9f1-630af15b879c" });
        }
    }
}
