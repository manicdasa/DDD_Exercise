using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class birthdate_applicationuser_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ecbce56d-3ff6-4704-9ef1-8819fb36f416");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "22e37ee6-d7e0-4eb9-bf44-81ac2b8ed282");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7c90779f-0e5c-4439-8d60-561844b95b54");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac0024d6-e54e-4172-b900-1de1c58e7b8b", "AQAAAAEAACcQAAAAEF2II67eWvENfEFtVRMJl+759jymCtFbJg0M9RydYCfG/ei9W3dUAAzLpxfvVg9g1Q==", "686e312b-1191-4ba8-a05c-7aba6d824235" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b25c72fd-9a5d-4407-a7df-0f6b8298c5ea", "AQAAAAEAACcQAAAAEN5p01SD+BtA6MbhA7nWEACX9EdyvV2esEteZfJt5OJ+s1bLMCIhkjSHcVUzpFoV4g==", "7b21ce36-709d-4ec4-8136-d4b861f3e636" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 23, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6fa56fa6-f507-4751-89b5-6763afaa8f64");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c2cb8cfe-1476-4d93-a4ae-ad46213ebcb1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6ff28413-b492-44f9-a02f-169ec9c20c04");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21479bbe-25eb-453e-bf09-b6f728333f8e", "AQAAAAEAACcQAAAAEFQbejLetleEMah87jCOdevLdYYPvwdR5dzUapnYdAl+awS9PmrEtSDh4229StJCgg==", "c8eec2ef-40bf-41d3-bd92-54e52513242a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91d56eea-8644-4710-a6d0-47ceae37c3d0", "AQAAAAEAACcQAAAAEBxK7aQrovni53DwEWj/LWv5wm9/DZ+yS4bTo+0ttvRtEtjwN22pJj4X+i9UmVaB7Q==", "7b24fc89-2ca4-4690-8ffc-f94a00b0de08" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
