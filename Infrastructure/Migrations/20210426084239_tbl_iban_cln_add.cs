using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class tbl_iban_cln_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "UserRoleData",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9f3a1dc1-b9cc-49a4-b54e-e2b5d0145900");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "60433aec-5804-441e-afb7-f48621ece474");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a05861a8-cfb5-4ec7-830f-93904cb62887");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f856998e-c85a-4cd3-88c8-0892a07cb7be", new DateTime(2021, 4, 26, 8, 42, 38, 423, DateTimeKind.Utc).AddTicks(9555), "AQAAAAEAACcQAAAAENnu1aRAA6ZF7VTeajfIDwrXzEhTFF1V96/hqO8gJV6QM4QBGwy1RXfRHcE4nN5teg==", "bf20c955-d97c-4043-aa1d-0f721ab754d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f366b9e-8e65-4fda-9f88-0f26b0a0da5a", new DateTime(2021, 4, 26, 8, 42, 38, 431, DateTimeKind.Utc).AddTicks(5356), "AQAAAAEAACcQAAAAEBHsZI51Y1gXmLNzf6iTNQT1aFq6S/x+/skA521ZUDEOpKufi84z/hC8MTp/vpD+bw==", "9544304a-5f01-4119-9ecb-77a97a72c09f" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "UserRoleData");

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
    }
}
