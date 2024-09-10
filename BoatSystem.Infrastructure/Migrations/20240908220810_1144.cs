using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoatSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1144 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Boats_BoatId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Owners_OwnerId",
                table: "Trips");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Boats_BoatId",
                table: "Trips",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Owners_OwnerId",
                table: "Trips",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Boats_BoatId",
                table: "Trips");

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
                name: "FK_Trips_Boats_BoatId",
                table: "Trips",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Owners_OwnerId",
                table: "Trips",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
