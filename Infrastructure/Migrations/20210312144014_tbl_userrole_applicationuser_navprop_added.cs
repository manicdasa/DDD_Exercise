using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class tbl_userrole_applicationuser_navprop_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "AspNetUserRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fbde7ea0-a279-491c-b6a8-eb894719c154");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "259f6528-3c3e-46b7-82b9-0f9e99456510");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "59aee982-3fbf-49ea-89be-d1ac268f25a2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d813b25b-e4c9-4bfa-805a-a394cb0dc3b6", "AQAAAAEAACcQAAAAEFOg83WKVwmHbu1PV7bXccb/nHO6jau6cXauSMcA029jViTpyUPMMTK5zap+B4TveQ==", "d677f907-d53c-460d-ba2c-81d7c782aa42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8cdb1c3b-ad09-4e7b-98a5-d0f308e47fdc", "AQAAAAEAACcQAAAAEF4sKRouVwwc7x1JcdC9IDUAsw91aFI9NM08mdBkQ+WWcUVtsoPrpV6+xfz/iqoJbQ==", "bf48457d-a3e8-427d-ab55-a58d72dc8f16" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "049e6c21-50f4-4e37-9fac-1d53b11fcb3d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "01a44c8a-7740-4d17-9835-4da19b7d1462");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5d5e1551-1841-45cc-b523-461a03a85970");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f370595d-aeb1-48b5-b1bb-692abf22b315", "AQAAAAEAACcQAAAAEOorZPYM7sK8o/EHOKw1mNRH/KEOsFMPuHOEo6e9W8OVxt6cfVg22sbGtx2BCwA4Sg==", "4f5f1bce-b9e7-4148-9428-369c60bb19b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85712c3d-0468-4906-9813-78029018b337", "AQAAAAEAACcQAAAAEGLyI6O+nMVFNlTF7L1les+GApmqOJquHT/CafRm4TpigyReVUHamTodG2X4HP1XWw==", "e356f194-814f-4c41-8ee8-6830c527bb71" });
        }
    }
}
