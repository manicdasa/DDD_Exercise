using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class document_update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_UploadedByUserId",
                table: "Document");

            migrationBuilder.AlterColumn<int>(
                name: "UploadedByUserId",
                table: "Document",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "58dcefe5-e088-451f-adff-63149e412466");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "13e1da28-3913-4882-a887-7f37710a6404");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "43598694-9634-43c0-8286-13f49efcebc2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dacecbc0-751c-4e0b-a268-403bb9bc68a4", new DateTime(2021, 4, 15, 13, 21, 15, 97, DateTimeKind.Utc).AddTicks(5882), "AQAAAAEAACcQAAAAEN9E7LsDGe/hCkN5x+MVnfzUk6vhGZLMDiBCKPnJuIgUQKcAoBsPU8o/zn+P4dsxKA==", "c7a12e6d-f352-4cca-854a-acf7cbaa0f50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "beb86857-6eb4-423f-acc8-5f70c175b1e6", new DateTime(2021, 4, 15, 13, 21, 15, 104, DateTimeKind.Utc).AddTicks(9984), "AQAAAAEAACcQAAAAEMyVh2/SEl9Ju/1frdCxIESTPPzKPeDnDTKI7AfVEg+2lOL5lHLjmcWD+LqYK+uYQg==", "86192266-aafc-4817-8e58-892535eb9426" });

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_UploadedByUserId",
                table: "Document",
                column: "UploadedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_UploadedByUserId",
                table: "Document");

            migrationBuilder.AlterColumn<int>(
                name: "UploadedByUserId",
                table: "Document",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_UploadedByUserId",
                table: "Document",
                column: "UploadedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
