using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class tbl_plagiarismcheck_cln_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DownloadEndPointUri",
                table: "PlagiarismCheckInformation",
                newName: "Status");

            migrationBuilder.AddColumn<long>(
                name: "TotalExcluded",
                table: "PlagiarismCheckInformation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalWordsScanned",
                table: "PlagiarismCheckInformation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ecff4391-39d8-4dcb-b697-f36c02f5b73a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7f4820c9-ee09-4d95-affa-75e5a62dace8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "204c6305-5dd1-45f6-980b-9793e16acc63");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3fc8b911-aa60-44d8-a0ac-3e9c1f909922", new DateTime(2021, 4, 22, 9, 46, 40, 917, DateTimeKind.Utc).AddTicks(7382), "AQAAAAEAACcQAAAAENcpTOvfS2pOFaHtZ9GXXW6eQi+8n1s9/s5NLEUgbmwY74OniXFIkNBCreGyPUipGA==", "cc16a9e7-c0b5-4b97-9e3e-5e3ec1d1f929" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e89f8908-2360-451c-ab66-d845cae09f60", new DateTime(2021, 4, 22, 9, 46, 40, 926, DateTimeKind.Utc).AddTicks(951), "AQAAAAEAACcQAAAAEKVJXAdpkXUJMqLF99AGEpYGSW9M9Iz3v3GLwKH3wA8rZ0oQykFUU8bVi32x/bBB6g==", "39353170-6b6a-49f9-8838-187a48fa4aba" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalExcluded",
                table: "PlagiarismCheckInformation");

            migrationBuilder.DropColumn(
                name: "TotalWordsScanned",
                table: "PlagiarismCheckInformation");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PlagiarismCheckInformation",
                newName: "DownloadEndPointUri");

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
    }
}
