using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class chat_tbl_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SentByUserId",
                table: "Message",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8b794c0e-a37c-47b3-a6eb-eee845cf4aec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4a39835a-4bfb-4d67-b320-67f46c1a414f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "72b60881-2b43-4417-8d1e-3a5ede8771c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ecd8cea4-9e2c-445a-bfaf-5cadaa0cbafc", new DateTime(2021, 4, 5, 13, 4, 49, 772, DateTimeKind.Utc).AddTicks(7230), "AQAAAAEAACcQAAAAEIKxzN/c0IY3Slten0GMTv+r2DSYewypgc3TaUKH9oJcCtIedQFWVJJioF7NtaUUVA==", "9273ffc5-a768-460f-9494-5d0f0347eb5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24fd736e-9a1a-43b9-9d5a-bc6fefef9e94", new DateTime(2021, 4, 5, 13, 4, 49, 781, DateTimeKind.Utc).AddTicks(45), "AQAAAAEAACcQAAAAEMuHs0ON0aU7Uj+Wve18ByHZKRZ94Ddlaq3s9k2QI1ItDJQTtwBjRnyXik2Gwa96KQ==", "0f97b7dc-fc08-4f66-b407-2c8fa5c94f93" });

            migrationBuilder.CreateIndex(
                name: "IX_Message_SentByUserId",
                table: "Message",
                column: "SentByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_SentByUserId",
                table: "Message",
                column: "SentByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_SentByUserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_SentByUserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SentByUserId",
                table: "Message");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f595e069-1b93-471e-a9f8-6e226dee896d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "752d3745-7079-42e5-9567-fe01cc1d0bc2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4327c86a-039c-4169-8d87-42345d68a4d4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70014924-ec21-41b5-9fb6-c1b21bff6045", new DateTime(2021, 4, 5, 10, 32, 38, 900, DateTimeKind.Utc).AddTicks(7275), "AQAAAAEAACcQAAAAEDavAQ4bqC3kT5UC6AWPn4LUBio/Vxfdaip8UVi8ebl5rGCZ3Kjt8VPr8A9k8Ba4Gg==", "417ad131-49e6-4958-8b42-d99c16b266fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "765a52ce-6ea2-4cd1-9472-95e6e32c8ce4", new DateTime(2021, 4, 5, 10, 32, 38, 908, DateTimeKind.Utc).AddTicks(1853), "AQAAAAEAACcQAAAAEMZH3RT4hHihlfw+m7Oxr/Mjfv4F26aomTAYh32r/CgdYgbLZD4rp8cR0E1IxYocNw==", "a5cd8bd1-5e69-4ec5-962d-4a1a7cdcafa7" });
        }
    }
}
