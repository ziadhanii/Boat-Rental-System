using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _85 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatBookings_Boats_BoatId",
                table: "BoatBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingAdditions_Additions_AdditionId",
                table: "BookingAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingAdditions_BoatBookings_BookingId",
                table: "BookingAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Cancellations_BoatBookings_BookingId",
                table: "Cancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_Cancellations_Reservations_ReservationId",
                table: "Cancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationAdditions_Additions_AdditionId",
                table: "ReservationAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationAdditions_Reservations_ReservationId",
                table: "ReservationAdditions");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId",
                table: "Owners");

            migrationBuilder.CreateTable(
                name: "TripBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    CancellationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CanceledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripBookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripBookings_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d5b6965-9c13-4e09-bdc7-d210eb5954ea", "AQAAAAIAAYagAAAAELB6Ll/RQRSnMeie/u9h+QogBqa0R9ep1v2D95lFh5f0ezslssePEoRMGeOHmVcuPQ==", "282d2bee-4804-4fbd-90ce-8fa6e8cea634" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a75514e-6079-4812-8a69-8e7e7a781f9d", "AQAAAAIAAYagAAAAEHSaPAzhvW5WRkjXwzu7FBXpRBqGElIRy8KJAUUaUOUDbb433GACTeLpW2rHNeAVyg==", "5a597172-c416-41f8-a916-1b9dfee41ed1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4884e99a-101f-4385-b4c8-45cfdfb1e4b3", "AQAAAAIAAYagAAAAEEyJ+iWnfuG0VnmV93npYNGUUaH5eCH2qo7X9QQRHVj7346npYGRAi5Dkd8D5xQ1zg==", "326437a7-7b0f-4c0e-a191-50320a6964e0" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "FirstName", "LastName", "UpdatedAt", "UserId", "WalletBalance" },
                values: new object[] { 2, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(1143), "John", "Doe", new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(1143), "3", 500.00m });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Address", "BusinessName", "CreatedAt", "IsApproved", "IsVerified", "UpdatedAt", "UserId", "WalletBalance" },
                values: new object[] { 1, "123 Marina Bay", "Nautical Ventures", new DateTime(2024, 9, 11, 0, 33, 48, 962, DateTimeKind.Utc).AddTicks(5673), false, false, new DateTime(2024, 9, 11, 0, 33, 48, 962, DateTimeKind.Utc).AddTicks(5674), "3", 1000.00m });

            migrationBuilder.InsertData(
                table: "Boats",
                columns: new[] { "Id", "Capacity", "CreatedAt", "Description", "IsApproved", "Name", "OwnerId", "ReservationPrice", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 10, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(2373), "A beautiful sailboat.", true, "Sea Breeze", 1, 150.00m, "Available", new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(2374) },
                    { 2, 8, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(2378), "A powerful motorboat.", true, "Ocean Explorer", 1, 200.00m, "Available", new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(2378) }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "BoatId", "CancellationDeadline", "CreatedAt", "Description", "DurationHours", "MaxPeople", "Name", "OwnerId", "PricePerPerson", "StartedAt", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 14, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3982), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3992), "A relaxing trip to tropical islands.", 4, 10, "Tropical Getaway", 1, 200.00m, new DateTime(2024, 9, 12, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3992), "Available", new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3993) },
                    { 2, 2, new DateTime(2024, 9, 16, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3996), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3997), "An exciting cruise with lots of adventures.", 6, 8, "Adventure Cruise", 1, 300.00m, new DateTime(2024, 9, 13, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3997), "Available", new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3998) }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "BoatId", "CanceledAt", "CreatedAt", "CustomerId", "NumPeople", "ReservationDate", "Status", "TotalPrice", "TripId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6535), 2, null, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6534), "Confirmed", null, 1, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6535) },
                    { 2, 2, null, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6539), 2, null, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6539), "Pending", null, 2, new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6540) }
                });

            migrationBuilder.InsertData(
                table: "TripBookings",
                columns: new[] { "Id", "BookingDate", "CanceledAt", "CancellationDeadline", "CreatedAt", "CustomerId", "NumberOfPeople", "Status", "TripId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9327), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9329), 2, 0, 1, 1, new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9330) },
                    { 2, new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9331), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9332), 2, 0, 0, 2, new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9332) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TripBookings_CustomerId",
                table: "TripBookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TripBookings_TripId",
                table: "TripBookings",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatBookings_Boats_BoatId",
                table: "BoatBookings",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingAdditions_Additions_AdditionId",
                table: "BookingAdditions",
                column: "AdditionId",
                principalTable: "Additions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingAdditions_BoatBookings_BookingId",
                table: "BookingAdditions",
                column: "BookingId",
                principalTable: "BoatBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cancellations_BoatBookings_BookingId",
                table: "Cancellations",
                column: "BookingId",
                principalTable: "BoatBookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancellations_Reservations_ReservationId",
                table: "Cancellations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationAdditions_Additions_AdditionId",
                table: "ReservationAdditions",
                column: "AdditionId",
                principalTable: "Additions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationAdditions_Reservations_ReservationId",
                table: "ReservationAdditions",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatBookings_Boats_BoatId",
                table: "BoatBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingAdditions_Additions_AdditionId",
                table: "BookingAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingAdditions_BoatBookings_BookingId",
                table: "BookingAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Cancellations_BoatBookings_BookingId",
                table: "Cancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_Cancellations_Reservations_ReservationId",
                table: "Cancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationAdditions_Additions_AdditionId",
                table: "ReservationAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationAdditions_Reservations_ReservationId",
                table: "ReservationAdditions");

            migrationBuilder.DropTable(
                name: "TripBookings");

            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId",
                table: "Owners");

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Boats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boats",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "545d6f34-9b7a-40ff-b845-a396f37142f2", "AQAAAAIAAYagAAAAECMfc5ImDQu7UfYzw2x7sUDRWRSEnzC1CBdgEIn1rTlH66RAicU/DW3qovaE9FOnJw==", "71a39152-b531-4ca1-aff3-fad0dcb8a8b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2dc3b37-725e-4ea3-af88-3200f6fc6adb", "AQAAAAIAAYagAAAAEDW35++sc3S3oYJWAxsturJlFmR8Q3gr/vnbEyy4E5TbfyMKtUIasnegezrCO0k+hA==", "5e8795c4-a152-4e2f-b971-11afdb060c49" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "926a5a01-8f52-4b6c-88f7-78b87f478ba8", "AQAAAAIAAYagAAAAEOg6GJG/RuIZ437+9fPyuKxk15WDL1ulxfm0JcN/TNfcJEiqogGmOJ0wrlH3ihnnTw==", "3cb2be19-be25-4d6b-afa0-68e8c22817af" });

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatBookings_Boats_BoatId",
                table: "BoatBookings",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingAdditions_Additions_AdditionId",
                table: "BookingAdditions",
                column: "AdditionId",
                principalTable: "Additions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingAdditions_BoatBookings_BookingId",
                table: "BookingAdditions",
                column: "BookingId",
                principalTable: "BoatBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cancellations_BoatBookings_BookingId",
                table: "Cancellations",
                column: "BookingId",
                principalTable: "BoatBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cancellations_Reservations_ReservationId",
                table: "Cancellations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationAdditions_Additions_AdditionId",
                table: "ReservationAdditions",
                column: "AdditionId",
                principalTable: "Additions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationAdditions_Reservations_ReservationId",
                table: "ReservationAdditions",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
