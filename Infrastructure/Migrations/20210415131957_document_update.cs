using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class document_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaypalEmail",
                table: "UserRoleData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalPayerID",
                table: "UserRoleData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalVersion",
                table: "Document",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UploadedByUserId",
                table: "Document",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cf4298fb-bc4d-4b56-9889-700f09ef71b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "63cddcec-fdc8-45ac-98f3-db93bb511213");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b1677e1d-980e-47c6-9e54-1a8f2d2522c7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11a21a75-9b6c-4b95-9157-f1385cbfbc3d", new DateTime(2021, 4, 15, 13, 19, 56, 729, DateTimeKind.Utc).AddTicks(1140), "AQAAAAEAACcQAAAAEIGZOl7LG3WgvZgRIqo2njHviQdcOib9jUlqbJ12ht4AV6/UY2wAkYei5ro3QZAQFA==", "5a796d30-e071-42e6-9f75-52614c88f1ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c03071d-94c5-4c24-8dbd-29dd020c7a15", new DateTime(2021, 4, 15, 13, 19, 56, 737, DateTimeKind.Utc).AddTicks(37), "AQAAAAEAACcQAAAAEBYPeFRflop2btWdcnuPRRpVkE/NYIZvWr2jZZhEM3yZOaBLGlpbm5fffj6X9N3GRQ==", "d7f87a21-310e-4654-a5c5-063bcfa2ec07" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Document_UploadedByUserId",
                table: "Document",
                column: "UploadedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_UploadedByUserId",
                table: "Document",
                column: "UploadedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_UploadedByUserId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_UploadedByUserId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "PaypalEmail",
                table: "UserRoleData");

            migrationBuilder.DropColumn(
                name: "PaypalPayerID",
                table: "UserRoleData");

            migrationBuilder.DropColumn(
                name: "IsFinalVersion",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "UploadedByUserId",
                table: "Document");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "77d22ab2-9497-4767-b630-8ae4b6aad2bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5701d385-3e71-46fc-9fc5-f63c881b04a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b13be0f6-0dd2-4381-a9c4-2933c4be557e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3aecc43-9853-4e34-b8a0-543fdb922802", new DateTime(2021, 4, 12, 13, 2, 11, 103, DateTimeKind.Utc).AddTicks(8347), "AQAAAAEAACcQAAAAEC5nHY3vtzTKKe16Qxa5zZ4+QUPIHgJEnLKH/Tf2sX9qS9ZPffwp0nO8z67dqh+DNA==", "3bf1c9d5-3c86-4b17-97bf-cdd86e2fe986" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "453b57cd-2c96-43a8-b911-c8be1b907f25", new DateTime(2021, 4, 12, 13, 2, 11, 112, DateTimeKind.Utc).AddTicks(2767), "AQAAAAEAACcQAAAAEFb4OI33TeQElcL27yL/bEjRmIazSa6amu1HnnpqhFUfUv29tCMKmWQ/D8cy/DY/FQ==", "974b2bf4-e7ee-4d13-82e3-f47d83af29a8" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
