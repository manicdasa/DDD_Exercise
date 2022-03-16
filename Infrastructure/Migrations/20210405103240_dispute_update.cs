using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class dispute_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisputeStatus",
                table: "Dispute",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f595e069-1b93-471e-a9f8-6e226dee896d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "752d3745-7079-42e5-9567-fe01cc1d0bc2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4327c86a-039c-4169-8d87-42345d68a4d4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70014924-ec21-41b5-9fb6-c1b21bff6045", new DateTime(2021, 4, 5, 10, 32, 38, 900, DateTimeKind.Utc).AddTicks(7275), "AQAAAAEAACcQAAAAEDavAQ4bqC3kT5UC6AWPn4LUBio/Vxfdaip8UVi8ebl5rGCZ3Kjt8VPr8A9k8Ba4Gg==", "417ad131-49e6-4958-8b42-d99c16b266fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "765a52ce-6ea2-4cd1-9472-95e6e32c8ce4", new DateTime(2021, 4, 5, 10, 32, 38, 908, DateTimeKind.Utc).AddTicks(1853), "AQAAAAEAACcQAAAAEMZH3RT4hHihlfw+m7Oxr/Mjfv4F26aomTAYh32r/CgdYgbLZD4rp8cR0E1IxYocNw==", "a5cd8bd1-5e69-4ec5-962d-4a1a7cdcafa7" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisputeStatus",
                table: "Dispute");

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

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
