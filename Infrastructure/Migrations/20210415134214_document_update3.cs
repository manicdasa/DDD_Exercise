using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class document_update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8c443053-f8d5-4f60-84c1-c445230ca00c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3292c07d-b1eb-4b5c-ab2e-d9dbab6e44d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4d69c535-5e3e-4bb3-ba5a-be97bb1737d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c4c479a-3aa1-43b6-913d-e89a2b73c662", new DateTime(2021, 4, 15, 13, 21, 54, 121, DateTimeKind.Utc).AddTicks(9614), "AQAAAAEAACcQAAAAECB8cQptwBeARGE8Lvkywpcjsx/roFdTXxSMLr61YqUNH3drSpGVMCIVywLl036jww==", "33458791-335c-43d6-8853-df5e7c49c00e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3fb99fba-d1da-4eca-93e0-5fb2cd43bc92", new DateTime(2021, 4, 15, 13, 21, 54, 130, DateTimeKind.Utc).AddTicks(5699), "AQAAAAEAACcQAAAAEFySZjEngoKp7aXyI5Rzv7J+BpKIJsu/UFdki86pg+t0ZUp/uYPg7U/FgOKK55u96w==", "11a8fe63-e35e-4fbb-bfa6-1fcf7f510941" });
        }
    }
}
