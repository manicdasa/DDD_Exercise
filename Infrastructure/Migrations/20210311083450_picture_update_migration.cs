using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class picture_update_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VirtualPath",
                table: "Picture",
                newName: "PictureFileName");

            migrationBuilder.AddColumn<string>(
                name: "LocalPath",
                table: "Picture",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "24f5e17c-e0e3-461e-b978-54916f831a97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "30663f5a-da78-485e-9649-6530848fd242");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e2a8e926-21cb-4795-be39-8c51d6776d46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01b54c49-00e5-4b6e-9c5b-a102479005cc", "AQAAAAEAACcQAAAAEL/IT3OPO83JS7UbxSW9BvCpYnQ27A7a8xEbcdLlbia1wSicViAIzJEQIt3dN1QVfQ==", "7aa85aec-0098-49ce-91c9-a904723b8976" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37ee525b-531f-4e66-ad82-58d4fd9bb6a8", "AQAAAAEAACcQAAAAECtO5XMeKyJF06UKvgJciT6Cn+SS/6Wj5C5oH88SZl5UmtKksymgVyfFH3Z8m/BmkA==", "0e12615f-6f2d-4f6f-95ac-65638068dcd6" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalPath",
                table: "Picture");

            migrationBuilder.RenameColumn(
                name: "PictureFileName",
                table: "Picture",
                newName: "VirtualPath");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ef455fd0-da3c-4544-b39e-cb7470aab258");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "319d0bf2-900f-4682-a223-18bf929337a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "15fed5e5-d6a2-4392-929e-226da7fb581c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c991223e-55cd-45b8-bd79-b6ec185daa16", "AQAAAAEAACcQAAAAEFPPvUDj9xZ2gkXHCOyuE463QTC+haKTX913zhQpo453JhjgDsvpgQhLnOPQMLG9qQ==", "5b27c094-743b-4fb3-a92a-69d6a30ef8e1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "151e0977-7721-4334-90fa-8abc523d8b6d", "AQAAAAEAACcQAAAAEChQy63m827niLDWItU9yLhkh9i6LMVbA2O5BV+4jVtJeZ66DLw7HJ3OASEVPVJ5aA==", "e9ec1dbb-4126-4ba6-9790-089159b38554" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
