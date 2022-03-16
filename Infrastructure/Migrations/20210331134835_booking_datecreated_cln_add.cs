using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class booking_datecreated_cln_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Booking",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2da8f81f-3698-467f-bf98-0e9c99a3c3a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bac6070b-7d15-4b57-95e6-4254a6e81838");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a5fdeb7f-b340-43ea-8328-1e84aef26dea");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72881c1a-fd5e-4742-8e46-f62b02699b56", new DateTime(2021, 3, 31, 13, 48, 33, 897, DateTimeKind.Utc).AddTicks(9874), "AQAAAAEAACcQAAAAEELKupgGUdH9FXok/Vw+xLR+jEdef2gm+9CsuqiCEukeDLnHRyXsoZj3IJNro/oy0Q==", "e28237b0-b341-4dd5-9aac-e280649968fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f70f22f-1cb6-4712-9007-5bb2f2c1ccc8", new DateTime(2021, 3, 31, 13, 48, 33, 910, DateTimeKind.Utc).AddTicks(39), "AQAAAAEAACcQAAAAEBojsIXL+0T64xxWcKUuI39WzbR5o5IT6k/oFhV8wAJtH7eCmmP8W+KB3LVgToGCMg==", "f9aee0e1-a811-4f46-a836-f35cc22b1181" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Booking");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ec2839dd-4017-462f-ba18-0fcf2c5ca329");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "197dada8-a48b-4b2e-85dd-0b339ac05b8e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2d78476d-2c48-4a66-8030-d10a8fcc1aee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22a28a90-66bc-4b40-9549-946db90ddc13", new DateTime(2021, 3, 31, 8, 47, 28, 840, DateTimeKind.Utc).AddTicks(119), "AQAAAAEAACcQAAAAEJtp8fdgGyyReNZwH5ejAY05SFLDY8TLeODXLfv3Vw8TsFyokKkhnDXWsy+yRFRpuw==", "00958e51-3b87-4985-a71e-4260bcf5fcea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dffdf15a-4d6f-4bdd-833f-0f35fc2b6184", new DateTime(2021, 3, 31, 8, 47, 28, 848, DateTimeKind.Utc).AddTicks(3054), "AQAAAAEAACcQAAAAECn950ABbuqodM8szcGxRha68R636OZ4YWqykkpPxPorxn0bj9Rs35HAwnD86WkpdQ==", "07f1ad11-96ae-4184-a49d-36d08b419519" });
        }
    }
}
