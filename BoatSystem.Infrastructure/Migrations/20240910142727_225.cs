using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _225 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatBookings_Trips_TripId",
                table: "BoatBookings");

            migrationBuilder.AddColumn<int>(
                name: "DurationHours",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "BoatBookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BoatBookings_Trips_TripId",
                table: "BoatBookings",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatBookings_Trips_TripId",
                table: "BoatBookings");

            migrationBuilder.DropColumn(
                name: "DurationHours",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "BoatBookings",
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
                values: new object[] { "29c62ca0-bbd6-4cfa-8f8f-3c160ad19b0f", "AQAAAAIAAYagAAAAEHB3V6/X6uF1BzSiRTkxlnKQnR7xAZKs8cx6eBBJIotTBo+L9OCOA6KYfGyi41z/ZQ==", "240426e5-2e77-4bc5-9928-5e14d1d5f68a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "810aefa1-123a-4126-a70f-9b4df52a9837", "AQAAAAIAAYagAAAAEA6rNtgM8nA+ZP9/s9USh6z+hXjMR09aPWjdkYD9dVwjNTkrPSvuoPF/FHHdyaloAg==", "31547064-61f7-455b-beb2-707480726026" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1c44980-50f4-4123-9a4e-bcdf3987c7fb", "AQAAAAIAAYagAAAAEGNPO6UvJXeLrJuQ7LLHiNENOgnliPpUyIcOA7urZJ4rgNvbwLcY3Ec5U+dkOTKV9w==", "d9887923-54ae-4485-bad2-11ebf5455352" });

            migrationBuilder.AddForeignKey(
                name: "FK_BoatBookings_Trips_TripId",
                table: "BoatBookings",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
