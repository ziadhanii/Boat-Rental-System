using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _86 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96b520fb-e747-486b-89c7-6f143f089956", "AQAAAAIAAYagAAAAEPoZxyyPtiNb8vYGax/i9KdEI1yMmRQZn7JVYUxASjRrMJfau6WKr8OhhCkfDX8yOg==", "eeab729d-de09-45ec-a9dd-ec49b86b9473" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d4d5ee8-cc46-4c0d-a833-57c15de0182c", "AQAAAAIAAYagAAAAEKugnIcoc1zPjPKMy5KSvcESLmHgc+RzKJgKOUEXakYqkylGsX3DZEPdEIbalYhrbg==", "f0aa6965-df4d-4c48-82e0-bf67c73bf1e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be43679e-8e42-4452-9b80-216dc0f073e8", "AQAAAAIAAYagAAAAEDCyd4rqhxq6aTSm41oWyDrod/D6ra/wZMD2ZDOJkirhKoOntkaIvr2Qnq3oOm9ziw==", "a6f294b6-2d46-4612-843e-a26487d61e86" });

            migrationBuilder.UpdateData(
                table: "Boats",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(2727), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(2728) });

            migrationBuilder.UpdateData(
                table: "Boats",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(2731), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(2731) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(2132), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(2133) });

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 40, 11, 445, DateTimeKind.Utc).AddTicks(9119), new DateTime(2024, 9, 11, 0, 40, 11, 445, DateTimeKind.Utc).AddTicks(9124) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ReservationDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5023), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5022), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5024) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ReservationDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5026), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5025), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5026) });

            migrationBuilder.UpdateData(
                table: "TripBookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookingDate", "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5694), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5695), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5695) });

            migrationBuilder.UpdateData(
                table: "TripBookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingDate", "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5698), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5698), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(5699) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CancellationDeadline", "CreatedAt", "StartedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 14, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(3623), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(3630), new DateTime(2024, 9, 12, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(3629), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(3630) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CancellationDeadline", "CreatedAt", "StartedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 16, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(3634), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(3635), new DateTime(2024, 9, 13, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(3634), new DateTime(2024, 9, 11, 0, 40, 11, 446, DateTimeKind.Utc).AddTicks(3635) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Boats",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(2373), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(2374) });

            migrationBuilder.UpdateData(
                table: "Boats",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(2378), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(2378) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(1143), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(1143) });

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 33, 48, 962, DateTimeKind.Utc).AddTicks(5673), new DateTime(2024, 9, 11, 0, 33, 48, 962, DateTimeKind.Utc).AddTicks(5674) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ReservationDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6535), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6534), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6535) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ReservationDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6539), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6539), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "TripBookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookingDate", "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9327), new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9329), new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9330) });

            migrationBuilder.UpdateData(
                table: "TripBookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingDate", "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9331), new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9332), new DateTime(2024, 9, 11, 0, 33, 48, 966, DateTimeKind.Utc).AddTicks(9332) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CancellationDeadline", "CreatedAt", "StartedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 14, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3982), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3992), new DateTime(2024, 9, 12, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3992), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3993) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CancellationDeadline", "CreatedAt", "StartedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 16, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3996), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3997), new DateTime(2024, 9, 13, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3997), new DateTime(2024, 9, 11, 0, 33, 48, 965, DateTimeKind.Utc).AddTicks(3998) });
        }
    }
}
