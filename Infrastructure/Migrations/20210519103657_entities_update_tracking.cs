using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class entities_update_tracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Proposal",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Project",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "HeadProposal",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "HeadProposal",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Booking",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b2e947fa-00ae-42e0-8d1b-671e8e7bc5c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "58c77268-27b7-47f4-b8cc-9d88ee235338");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8a34b0f6-3d4f-4c8f-8160-c2314056e97e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52cd67bc-e193-49c2-9f05-596b1f472615", new DateTime(2021, 5, 19, 10, 36, 56, 337, DateTimeKind.Utc).AddTicks(1556), "AQAAAAEAACcQAAAAEFmmxNoCDOaWonN/nsdjotyO6kAqjc5ox2tFBa6QgNOVzceU4JltpCicujrnFxSL8Q==", "10d564a4-2c0a-4177-bf8a-a3e34f67af03" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a37c474c-f8ba-4afd-9dc8-451b757e7bd0", new DateTime(2021, 5, 19, 10, 36, 56, 345, DateTimeKind.Utc).AddTicks(9256), "AQAAAAEAACcQAAAAEPyqhtpz+nQkcmRf08WhjclKMtFOIq3kYFMus7pApTU9oqw3cZTcAJrBAuMcOgmCpw==", "e69d9361-f2d4-47ec-952a-5be508e4354a" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "HeadProposal");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "HeadProposal");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Booking");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6752bb1e-bb95-48fc-8799-9f45d396d294");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "30462d73-f717-4e2b-9fd5-fbe0c851a392");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7779d834-77b5-4288-a1d0-39506a66a16e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbff5fea-d8cd-40d6-99f0-56c4ab0a779c", new DateTime(2021, 5, 18, 10, 56, 34, 924, DateTimeKind.Utc).AddTicks(4386), "AQAAAAEAACcQAAAAEExZ/IuB8Q93oLykR4vq2rMSGH/kaqlMKTe8iY3b8wKHngjA/3QyLF6FwZIrOMsQtw==", "892c5125-eb95-4a08-9c5a-1bb293159fee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18ed3126-170f-4456-98c5-f9ea3fd417c8", new DateTime(2021, 5, 18, 10, 56, 34, 931, DateTimeKind.Utc).AddTicks(8160), "AQAAAAEAACcQAAAAEOpInK3JkvT7IfZos+AXj7tIOw37JkYNYjwWJwhLmIx710V/FaHRMGx19/mz67PTMg==", "c13f0dfa-4ff5-4ed9-9503-8fea4fd11bc3" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
