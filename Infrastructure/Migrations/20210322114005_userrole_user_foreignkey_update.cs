using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class userrole_user_foreignkey_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_UserRoleData_UserRoleDataId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "ApplicationUserRole");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_UserRoleDataId",
                table: "ApplicationUserRole",
                newName: "IX_ApplicationUserRole_UserRoleDataId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "ApplicationUserRole",
                newName: "IX_ApplicationUserRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRole",
                table: "ApplicationUserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b0587c13-3689-4d4d-a8b8-af2d55a81185");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "25d10ff3-1581-4006-81b7-d0a6bed1d444");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "658d2193-7969-40d7-b285-9d1614ab6a64");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6868c31c-7570-4a51-8330-fdc36a757b06", "AQAAAAEAACcQAAAAECstwF7xNCOyG2OsMZOCKBYF5bqKY615g0DKnAP9F38QSDg88k+36oD6ZX6iIZbYOA==", "7a91e47a-563f-4886-a706-e8f0e03f0018" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7695fda2-21c0-4d13-81c2-e2f9b8cdb10c", "AQAAAAEAACcQAAAAEKSnAxUgQB72o1/qGe1svfSEveYI781tbcoChuAFrY+DceVL6DSVdgzbIg0aZih89A==", "2b74cb13-ad8d-4f11-8f94-47651b6c247d" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_AspNetRoles_RoleId",
                table: "ApplicationUserRole",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_AspNetUsers_UserId",
                table: "ApplicationUserRole",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_UserRoleData_UserRoleDataId",
                table: "ApplicationUserRole",
                column: "UserRoleDataId",
                principalTable: "UserRoleData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_AspNetRoles_RoleId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_AspNetUsers_UserId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_UserRoleData_UserRoleDataId",
                table: "ApplicationUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRole",
                table: "ApplicationUserRole");

            migrationBuilder.RenameTable(
                name: "ApplicationUserRole",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserRole_UserRoleDataId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_UserRoleDataId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserRole_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "AspNetUserRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "36413ede-30c5-4133-b89c-9ca721b0afe1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "041c0d01-ee89-494c-b11e-c761e097e264");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "607c525b-d1ad-4241-9ff9-fc283dbbe0cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5270c1df-41d4-48d9-b9a5-2c755e7b4770", "AQAAAAEAACcQAAAAENKUsa9SVSjEPTi/xAFoyxsJW5Nm2L/Y5br64OwyRC09+ZDF8WAQGn1PPWcLMRkjcA==", "a58d9bbd-f590-4b07-ae84-cfe97d9e0b7b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37e3ef53-bac8-480f-a487-7e41bdb17378", "AQAAAAEAACcQAAAAEIvhh5+rqP8s3DVIBiWnp4nM+/LmaYxMAB+gVixa91ojt8Uu8qOX7OQ2p2j0RCQhNQ==", "4d38d8c2-89f5-4046-a66c-0e17d8e043ed" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_UserRoleData_UserRoleDataId",
                table: "AspNetUserRoles",
                column: "UserRoleDataId",
                principalTable: "UserRoleData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
