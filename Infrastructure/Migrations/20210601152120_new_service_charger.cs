using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class new_service_charger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "60feb775-85d5-4416-b26f-812b7b1c13fc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0d1f03ec-1175-451b-91a7-334d868c6f1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2a4ad632-aff7-441d-b2e0-873bca6ccb4e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78cf10c2-1ccf-49d8-9e63-3e64335590ac", new DateTime(2021, 6, 1, 15, 21, 19, 508, DateTimeKind.Utc).AddTicks(3358), "AQAAAAEAACcQAAAAEBrM669eM9ksj6LhBeqWT7cnm4Oqfqd6N/c8ty7t+psg5DSxWG2zyzBRzZKE2c756g==", "ad424177-2f0e-47c6-87e5-5896a4c2e042" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "221e39d6-d6f8-4c76-a708-6402c8412995", new DateTime(2021, 6, 1, 15, 21, 19, 515, DateTimeKind.Utc).AddTicks(8591), "AQAAAAEAACcQAAAAEIg9Q14l6nokTtTIIvqxqFqq1RCOxalMayYNqfBabX/MHutZXokn5bq3ZhjtLdQ+Gw==", "7008927d-f88c-4d05-93a3-844e8b73ef9f" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChargeAmount", "IsPercentage" },
                values: new object[] { 19m, true });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ChargeAmount", "EndDate", "IsDefault" },
                values: new object[] { 9m, new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 1,
                columns: new[] { "ChargeAmount", "IsPercentage" },
                values: new object[] { 20m, false });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ChargeAmount", "EndDate", "IsDefault" },
                values: new object[] { 20m, new DateTime(2021, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc), false });
        }
    }
}
