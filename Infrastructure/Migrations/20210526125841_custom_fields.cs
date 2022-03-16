using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class custom_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FieldStatus",
                table: "KindOfWork",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FieldStatus",
                table: "ExpertiseArea",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4a406949-d357-4d22-a73f-1cda15d10312");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "65bf06cd-c7c1-4ef1-a1b8-2e741f68c640");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0488b7ef-be01-4ad9-b99f-049da9f843cd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f661d4f-5790-4eae-9622-a600ee8d49c7", new DateTime(2021, 5, 26, 12, 58, 40, 521, DateTimeKind.Utc).AddTicks(6597), "AQAAAAEAACcQAAAAELOceQiH3Po5Nvwrf+mnHCXkH7tO6XBxR/OQSrdGvwaNZB5D+ehGuFkcA6GZdJLJrg==", "ea5701fe-4bf6-4de8-9f3e-02aa3fa350e3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4eae600d-c309-44bc-8c4d-09994d5f15b8", new DateTime(2021, 5, 26, 12, 58, 40, 529, DateTimeKind.Utc).AddTicks(1269), "AQAAAAEAACcQAAAAEJrVflEfriIA6JjtbaVlKMhxXdCDIjBTr2DG58wD1HFTmqJJlKFTbe4O+h1HGCNL5w==", "b68cb9ea-a1f4-46a5-abd0-5e06353adbe3" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldStatus",
                table: "KindOfWork");

            migrationBuilder.DropColumn(
                name: "FieldStatus",
                table: "ExpertiseArea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b2e947fa-00ae-42e0-8d1b-671e8e7bc5c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "58c77268-27b7-47f4-b8cc-9d88ee235338");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8a34b0f6-3d4f-4c8f-8160-c2314056e97e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52cd67bc-e193-49c2-9f05-596b1f472615", new DateTime(2021, 5, 19, 10, 36, 56, 337, DateTimeKind.Utc).AddTicks(1556), "AQAAAAEAACcQAAAAEFmmxNoCDOaWonN/nsdjotyO6kAqjc5ox2tFBa6QgNOVzceU4JltpCicujrnFxSL8Q==", "10d564a4-2c0a-4177-bf8a-a3e34f67af03" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a37c474c-f8ba-4afd-9dc8-451b757e7bd0", new DateTime(2021, 5, 19, 10, 36, 56, 345, DateTimeKind.Utc).AddTicks(9256), "AQAAAAEAACcQAAAAEPyqhtpz+nQkcmRf08WhjclKMtFOIq3kYFMus7pApTU9oqw3cZTcAJrBAuMcOgmCpw==", "e69d9361-f2d4-47ec-952a-5be508e4354a" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
