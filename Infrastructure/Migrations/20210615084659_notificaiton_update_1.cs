using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notificaiton_update_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadProposalId",
                table: "Notification",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6488c75f-c6fc-4698-a332-b24e93c7c49b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cc7c9b69-df24-4850-a0d0-ac5fd790f4c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0c81d91b-0b60-41a0-b66c-0f24d74481be");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "638c2690-4cc7-40bc-b4d9-c0a695c65b23", new DateTime(2021, 6, 15, 8, 46, 58, 255, DateTimeKind.Utc).AddTicks(5145), "AQAAAAEAACcQAAAAEKd0SZ3guhZ0zc2S9mkuXoO0s7ygxLU+eQiLUeK4JEtSxYvtNEftdFpP/838apAMaw==", "a073dcc0-468c-43f5-8485-ccb5bad84919" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfc711e1-114c-4630-85b4-2f0056fd3f13", new DateTime(2021, 6, 15, 8, 46, 58, 270, DateTimeKind.Utc).AddTicks(2772), "AQAAAAEAACcQAAAAEHBvlDY2sb+rqnzZ4mh2wzb7yAL8ykDVkJ1a34ykiUki+mXf76nV+Uyk6PkDpDq8Wg==", "e9831b18-3795-4fa1-a9ba-ab3fcff98b09" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadProposalId",
                table: "Notification");

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
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
