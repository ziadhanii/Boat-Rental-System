using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _558445 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CanceledAt",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDeadline",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDeadline",
                table: "BoatBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanceledAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CancellationDeadline",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CancellationDeadline",
                table: "BoatBookings");

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
        }
    }
}
