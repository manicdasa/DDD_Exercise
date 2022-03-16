using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class firstname_lastname_applicationuser_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "UserRoleData");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "UserRoleData",
                type: "text",
                nullable: true);

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
        }
    }
}
