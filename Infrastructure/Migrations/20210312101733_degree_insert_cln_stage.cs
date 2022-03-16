using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class degree_insert_cln_stage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "Degree",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "049e6c21-50f4-4e37-9fac-1d53b11fcb3d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "01a44c8a-7740-4d17-9835-4da19b7d1462");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5d5e1551-1841-45cc-b523-461a03a85970");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f370595d-aeb1-48b5-b1bb-692abf22b315", "AQAAAAEAACcQAAAAEOorZPYM7sK8o/EHOKw1mNRH/KEOsFMPuHOEo6e9W8OVxt6cfVg22sbGtx2BCwA4Sg==", "4f5f1bce-b9e7-4148-9428-369c60bb19b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85712c3d-0468-4906-9813-78029018b337", "AQAAAAEAACcQAAAAEGLyI6O+nMVFNlTF7L1les+GApmqOJquHT/CafRm4TpigyReVUHamTodG2X4HP1XWw==", "e356f194-814f-4c41-8ee8-6830c527bb71" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stage",
                table: "Degree");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "72de7824-1cea-43cc-b18f-5b2d0992e828");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8a668bb5-3a08-4694-ab43-e2e2f67fb3a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4b9976f9-61cb-4984-a7e7-a725fc4eb459");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e0ed873-9b07-49ba-8198-c8efe001c328", "AQAAAAEAACcQAAAAEHxNcFsLZDULEM3bbhMzBM0TTvAy+vUkiFC2yD9s/suoJawt2P/Wg1ukpkT6oc2MfA==", "8c011d2c-9bb0-48ae-b1b1-607f45d80fb5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bed30f35-73ac-46a8-9c3a-64d979eb04b5", "AQAAAAEAACcQAAAAEBNQcsXCvRldsED2gUAPAh7MucJfC+PCqotEu+R8JhZV81D9fg7NQMwO1WzIyhLRhQ==", "8bde0212-e815-45a0-9c0a-cbf0f4b1ba6b" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
