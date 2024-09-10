using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1145 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Owners_OwnerId",
                table: "Trips");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe9d83dd-a781-461e-921c-d8224db2b0e7", "AQAAAAIAAYagAAAAEFXEBf3whgXzSWxxvB1tFZhoosz3Kp6Ngye1bUit++E6OQhZyRbj+QC0vQ1rK66qfg==", "fc06ea63-e69e-4fa4-8995-f1f2420841e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d60c990f-8739-4bec-9c45-f93f1d8fbb59", "AQAAAAIAAYagAAAAELJO5rqs5jDFVcOKO/4i0oSDZoOPsRm0AbT3TuIAMEvH2+Jfew2vukR3kEBcGLDzBQ==", "32aee3e6-ac44-41ac-b1cb-800ac49590cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7f22008-a7a2-4eb8-800f-1eba669a17c6", "AQAAAAIAAYagAAAAEOELgrSuW08BGxpnXGbDlc6eFVWuRPcPbhNHXlrc8AXUfrd+RmjLT+Q76LnnqM6zoA==", "ef9caa5c-6cd6-48ad-882e-2360d96196fa" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Owners_OwnerId",
                table: "Trips",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Owners_OwnerId",
                table: "Trips");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Owners_OwnerId",
                table: "Trips",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
