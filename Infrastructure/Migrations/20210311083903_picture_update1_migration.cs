using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class picture_update1_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Picture",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6d24e978-4870-4a4c-a510-8ed551468f07");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "85214152-9bb5-4581-b708-0c9c4f2d6469");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "01772618-a710-46a0-843c-74d00612af23");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87e271b2-24a9-4ee9-99fd-4a16dc52b820", "AQAAAAEAACcQAAAAENu7bLgsXrIcMNrYW7oJnYEloP9KUzOH+ohaBYJoDAhkt4Vsxb+WoJ2qkOgRDTPDFQ==", "283a1540-c6d9-41fe-a6bb-4fbde7095520" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe200599-990f-409f-804d-51560c6e5914", "AQAAAAEAACcQAAAAEMMmoxQpL9AxRzqRxFO8AYjOdYpASwqT2rMeOGuYu2zK/l5kVrBGPkkc7etbkA7kUQ==", "89219580-4426-4747-9e9c-8ff895fb5585" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Picture");

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
        }
    }
}
