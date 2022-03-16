using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class refund_dispute_cln_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RefundAmount",
                table: "Dispute",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "82cfefc6-a820-48b8-bf36-1b5ed6adf56e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "317b32ef-9c41-43f4-a87f-67ff8b038140");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5b218827-563c-430f-b7f3-a79794a01e79");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d1f0f6b-5d4c-42b9-b619-28c3895dcc96", new DateTime(2021, 4, 21, 9, 17, 19, 361, DateTimeKind.Utc).AddTicks(2159), "AQAAAAEAACcQAAAAECYzb1+lVcHqC59phzpRpB7LIF191bnmZsShB1W40VSyoVjzqQpoYKFvxPXndNusag==", "f0b5081f-7153-454c-b8b3-2e88b5ff71c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "734e83b5-7c08-49d6-b379-aea66c937183", new DateTime(2021, 4, 21, 9, 17, 19, 368, DateTimeKind.Utc).AddTicks(8658), "AQAAAAEAACcQAAAAEMUIdBvqG/S2ICFb7lGFoXFlqJuBOrGzKko1Hwr25KSqWQhdEI7p92QrdzK1qaBExg==", "fa9404a9-8934-4260-a522-4ca1cfc459a6" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 21, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundAmount",
                table: "Dispute");

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
    }
}
