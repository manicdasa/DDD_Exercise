using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class tbl_document_copyleaksscanid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CopyLeaksScanId",
                table: "Document",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "10ea4924-4552-4f5d-8709-079687f0dd79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d8bae804-491e-4725-a65f-cda67b4a9466");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1646b461-ff4c-4ce4-ac2e-574758f97aa8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "488d761e-f296-4f6b-9304-2d599dd1f5d2", new DateTime(2021, 4, 21, 11, 49, 58, 48, DateTimeKind.Utc).AddTicks(3402), "AQAAAAEAACcQAAAAENylOBDDf3e6EGrWHMD+XklEu6t1G7ADOmojUpZxGGx6tUxUCNhYXUvXQMMuxX/Q7g==", "9c0ce9d9-c18e-4452-b650-8ba10947a93a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02aa6e8a-ff4e-41a7-bc28-8cd625095e18", new DateTime(2021, 4, 21, 11, 49, 58, 55, DateTimeKind.Utc).AddTicks(8154), "AQAAAAEAACcQAAAAEJUgCYAgTJp2NZUDtwk6tUhd8wjRrtcedtiAOIdA6WT26W1ERNa9P7KQw/0LbFYf6A==", "d1deab1c-0e33-4b8c-b876-6dcdfafb75a9" });

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
                name: "CopyLeaksScanId",
                table: "Document");

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
