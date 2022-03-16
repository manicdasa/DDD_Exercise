using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class project_and_user_datecreated_cln_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Project",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8427c3e4-7835-4590-888a-234b82af484d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6c0aea5a-fdf9-4c96-820c-2b848abae1a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a5a9fe79-08a9-4023-83cf-3b5aca17a08d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da3737e8-37c4-49b4-836d-82d58dcb95d1", "AQAAAAEAACcQAAAAEFO34g/bxk8pDc1g09PLe3ruXfFGaNT4VaiDqOc74SSB8zU59auXD2F3iSiZpZqL9Q==", "25b2ab16-9937-4dbc-9a64-cb73d87f0ba3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "861715f4-d9f3-4f1c-ae6c-f48b1a3185c9", "AQAAAAEAACcQAAAAEEJ92ckGcbjdlKiHsFRn1pS4q6TtOIvlL5mtcYHUO3YXWk5E/PZAL36hQknaclu2aA==", "824806dc-faf5-45b1-9c66-cf45f40e657e" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 26, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
