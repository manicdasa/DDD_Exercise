using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification_update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeadProposalId",
                table: "Notification",
                newName: "HeadProposalIId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "42147d61-1da0-4b32-bd49-7e85ec35bc80");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1314391a-4333-4527-94d9-b7d80000bfde");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b1a33870-7942-4da6-b9f8-31750f409d8b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "161cbd87-ee7b-4386-a9eb-2f284680aa07", new DateTime(2021, 6, 15, 13, 34, 44, 425, DateTimeKind.Utc).AddTicks(3280), "AQAAAAEAACcQAAAAEEhp73sKVsoCKnulYwm7+gXBA+4AWtSRYcjKwLi+ZzPvUuGEl/g2OUAvqBsGT1oy6w==", "47db4cbd-d7c5-48b0-9363-1ebe74f9f2f3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "160b5eef-2f72-484d-b79a-de9513cf2d27", new DateTime(2021, 6, 15, 13, 34, 44, 432, DateTimeKind.Utc).AddTicks(8849), "AQAAAAEAACcQAAAAEDVp13+Oqcwr+ywGubtQV0EImlGJnBGFDR+sqAym+Jil0gfUPR2FXnj2WGj2Is+l8Q==", "8592e5c7-e95c-45d8-bdb2-9e156b6bb2ce" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeadProposalIId",
                table: "Notification",
                newName: "HeadProposalId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8632ed6a-d98b-49e8-ad94-a4a05b83038e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2e5b863d-f6dd-4c9d-84b8-cf083ec6d8dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "bf80066f-da25-42e8-a165-ec99f008aaa1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb31c836-3dcd-4778-9233-ad7d182afd0f", new DateTime(2021, 6, 15, 13, 32, 53, 522, DateTimeKind.Utc).AddTicks(865), "AQAAAAEAACcQAAAAEPxDIKgRBrhaYyTinaE1q2K51zc2Jp1SA+LJr67siFyE6xx39X9pBq7FQri7aAS6wg==", "ada97dc4-827e-42d9-a160-3050f7dc36be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a243692-ac88-461e-a55c-4c3bd6226788", new DateTime(2021, 6, 15, 13, 32, 53, 530, DateTimeKind.Utc).AddTicks(7312), "AQAAAAEAACcQAAAAENuYXLBoeX2CRpbCSYSe8MhFRPgAw3tRThnseXjGPg9mcB52AJ1EPC8nR2q7nSaM2g==", "2a3943bf-7572-40e3-b4de-3155a36d26ed" });
        }
    }
}
