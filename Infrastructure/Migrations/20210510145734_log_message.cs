using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class log_message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLogMessage",
                table: "Message",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ab896a2a-4db2-49fe-b3e4-c127eaab2de9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "80ab5b4c-a155-4455-a170-fb87782bb23d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "20b153c1-7c08-4d4e-bba9-75e8415f66b1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57bc0268-8078-4fa7-806b-2aa79b8d92ac", new DateTime(2021, 5, 10, 14, 57, 32, 805, DateTimeKind.Utc).AddTicks(9202), "AQAAAAEAACcQAAAAEJgOMiMaqPWAWXBws6nD7BXXmyuNPFTCn0wG3taQUKZvaiJh44s/30ldH2tL+mxG9A==", "c5e4c779-61e1-4f83-b67f-898e8b59ef65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "336d05ec-f91d-4626-af4c-9a08f89571d4", new DateTime(2021, 5, 10, 14, 57, 32, 813, DateTimeKind.Utc).AddTicks(3903), "AQAAAAEAACcQAAAAENMec47yP5JSKqc0U6/oowlkT/FusbfQx1Jqq/JW9NrfbiIgJ8pe0UB+yRpGJBQMeA==", "4dc6ed58-ef88-4ccd-9f8f-4a168f610030" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLogMessage",
                table: "Message");

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
    }
}
