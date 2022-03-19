using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class reconfigProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_CustomerId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_CustomerId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Project",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "72bafdd7-0101-4114-9f9e-e241917f7747");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ddcdffed-91dc-43b4-ab0c-f9162ffcde62");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0cad2158-3c49-4c2b-ab47-96d6f7608968");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8873a156-46be-4abb-8beb-f5d1002edd59", new DateTime(2022, 3, 19, 15, 0, 46, 451, DateTimeKind.Utc).AddTicks(6541), "AQAAAAEAACcQAAAAENsDKZSSs20z95aEQUN+qvwh0jhPT/bfp4mnM3k/5qwdNLWWpzBSKNcVLzUdd0wG5Q==", "f0f86179-3359-448c-9b10-06567301553c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af796945-51e7-4030-bc71-2a6c8a3ecf8a", new DateTime(2022, 3, 19, 15, 0, 46, 452, DateTimeKind.Utc).AddTicks(9463), "AQAAAAEAACcQAAAAED7oquuenyYw2NihO3aTRxTvFRt3jGdS7w0BQUkQBBWKIxnxv8rP5COjyC0oaeE7cQ==", "866daa5c-8227-4470-ad69-9796caa63861" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2022, 3, 19, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Project_ApplicationUserId",
                table: "Project",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_ApplicationUserId",
                table: "Project",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_ApplicationUserId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ApplicationUserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Project");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "28f61427-ef67-4a28-bb35-496775585c93");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c8d91a1b-0d63-438e-bc12-c4d9c3106948");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5afc6b0a-c9eb-4158-9522-a2160daada34");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9e96d3b-fc25-418b-91fb-bc997373ec78", new DateTime(2021, 7, 20, 9, 22, 48, 115, DateTimeKind.Utc).AddTicks(2808), "AQAAAAEAACcQAAAAEG7/tSaiCBzkF0wGCkztUpBYJYzUxeHDvK6Tg0U1F7GMsFV2Is9D8UhlkZ44fx/zpw==", "10627e8a-713c-4dcd-ab97-f445adf192c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66fdd4a8-5a6f-4870-ae48-f8d3e3332485", new DateTime(2021, 7, 20, 9, 22, 48, 116, DateTimeKind.Utc).AddTicks(9286), "AQAAAAEAACcQAAAAEKXLN/ByrTexwi1uh13NsvoBXhedcSDeyAfJPNo9whxINJPZFF5Gnpv2Fb2d8d/+Tg==", "96c17f26-e949-4ee3-8a4f-2c7d946e1c4e" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerId",
                table: "Project",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_CustomerId",
                table: "Project",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
