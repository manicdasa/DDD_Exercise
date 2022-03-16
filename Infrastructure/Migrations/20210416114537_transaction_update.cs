using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class transaction_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "Transaction",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "56749262-8484-4025-b2da-ee27547f4e08");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "13e32ff2-fe31-49f1-95f1-8d96a6cf3582");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7ddc3eec-aa93-4e9e-8d5e-dda26f92fe29");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07f3b91f-6389-486e-ae20-e08d776e2567", new DateTime(2021, 4, 16, 11, 45, 36, 658, DateTimeKind.Utc).AddTicks(2876), "AQAAAAEAACcQAAAAEIk9WaNLAfkWMOCDQT/vrkc1qLbiWs+5RQx8Ph5No0ZEYAFkc2Mu9WmXOn27RzTPrQ==", "19f1b7d3-6033-4011-90fa-ac123c3becb9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd3fc9a5-fcd3-45a3-b6cc-976448cef355", new DateTime(2021, 4, 16, 11, 45, 36, 665, DateTimeKind.Utc).AddTicks(8451), "AQAAAAEAACcQAAAAEMzmQlYK9NhtXPzb4KWKXSEq/hSl0JcZe91D3QoWO3yNi0qjNUyS+/p8kuUf3Vk5qA==", "058b55d2-501b-49eb-8f0e-da2d5f111c6f" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "Transaction");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3dadc14a-5648-4665-ba1e-3ba022a3ad31");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a77796c4-b35c-4609-8db1-2b23aa36937b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "10b5bb10-5558-44a2-b2ad-232437edcc6c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16004c0a-159d-466f-9fbd-cf54fa822966", new DateTime(2021, 4, 15, 13, 42, 13, 184, DateTimeKind.Utc).AddTicks(3389), "AQAAAAEAACcQAAAAED3Vj+rMFMM5GdMGui9/gLIfRH2N2mEqSem9d4k3ybQKpDU/TF3fLFdtgBOwPCtR1w==", "4b7d2f42-a2f1-45cd-8284-08354da49c80" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dff55bd1-42f6-4e99-96b4-806682e8135c", new DateTime(2021, 4, 15, 13, 42, 13, 192, DateTimeKind.Utc).AddTicks(6275), "AQAAAAEAACcQAAAAECSxD6khMCDNOT11ZGsnhuHBp1lBzRKfGZkV81NRe0VfKZZ+/8rwkxUo/wxvgjd/Dw==", "80af0155-396d-4921-b25c-ad6db09c17ab" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
