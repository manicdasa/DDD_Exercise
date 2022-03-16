using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification_update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HeadProposalId",
                table: "Notification",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HeadProposalId",
                table: "Notification",
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
                value: "77f4cd02-3ce7-4e28-bb97-35f70f991905");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0e999d7c-4552-4c2b-ac77-022bfcfc95c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1f7d9b21-e3ee-46eb-88f7-efae92c95330");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e61e9054-d8be-473f-90d4-424d761cdaa2", new DateTime(2021, 6, 15, 13, 29, 43, 369, DateTimeKind.Utc).AddTicks(5202), "AQAAAAEAACcQAAAAEH+9uPRto7JZ06m+k8hQgbKR/T9f0+H8V+lGW+i0nB4TLs5CMpmaYo7qK712COO6pg==", "656e8f42-283c-4ad2-8245-c2831152c8ff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9e133eb-3583-4c99-986b-be812656eec9", new DateTime(2021, 6, 15, 13, 29, 43, 377, DateTimeKind.Utc).AddTicks(5800), "AQAAAAEAACcQAAAAEM8A2bzuX3Q+5SJK189omQnI1lBLePsRPaFax74tivERzgVdrOS17A4NoATL87B3sw==", "645b5c49-0e75-4bea-83c3-65be622aa4f9" });
        }
    }
}
