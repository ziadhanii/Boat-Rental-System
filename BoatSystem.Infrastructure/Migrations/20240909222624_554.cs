using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _554 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "BoatBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_BoatBookings_TripId",
                table: "BoatBookings",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatBookings_Trips_TripId",
                table: "BoatBookings",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatBookings_Trips_TripId",
                table: "BoatBookings");

            migrationBuilder.DropIndex(
                name: "IX_BoatBookings_TripId",
                table: "BoatBookings");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "BoatBookings");

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
    }
}
