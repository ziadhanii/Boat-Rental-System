using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _5587 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3acb78f2-fbbb-43ab-8385-07901032fcd6", "AQAAAAIAAYagAAAAENpygvLg4IP5O4fS8KVaeS7hRn049mTxK3kvAl1h0yZrjgYw+CFdZ9Hvr/f6iJcUTQ==", "e587d03f-19ec-4fbe-b341-02e8c5014124" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7428203f-c6bf-4d0b-a20d-8e08cac30362", "AQAAAAIAAYagAAAAEEZyImDFD+DOBviCvzWRYoOMyDA6UeC5C6jEgx3ScBOnHTNyCfJd77SQWVhAUK84OQ==", "19476ea2-83f4-4551-ac4c-2b8e4f3e892b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73440e5a-ecfa-41cb-892a-2679a1582d9b", "AQAAAAIAAYagAAAAEOMUqKIaa0rwx4laHf8y9Gj/ZwKzwzTaG0CLv+2iBCTcEFqqkQMXoHuF5YAHwVXdgw==", "ee1daddb-cc7c-4f85-bed8-f472a5d50616" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TripId",
                table: "Bookings",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

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
    }
}
