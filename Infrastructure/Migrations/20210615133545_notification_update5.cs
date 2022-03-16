using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification_update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadProposalIId",
                table: "Notification");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9119151a-0976-4179-8e4d-5d04875efb64");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9877c804-5dac-4eb2-800f-27320b0078c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d9dcfddf-1939-490c-bf48-7d3104b0a44d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "463b37eb-f52a-4232-930d-e2bf2f16c036", new DateTime(2021, 6, 15, 13, 35, 44, 570, DateTimeKind.Utc).AddTicks(2605), "AQAAAAEAACcQAAAAEFt1ZjLH0NmfV7m7NO3Jej+G0ipWrkXKIrSK6B0wXUK2K9Qt6KE0QzTJoNvT7xtz0g==", "5f3208fb-a070-4aff-a944-e526209237d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a08d8d7b-ea1b-4d79-b622-4ca28475bd17", new DateTime(2021, 6, 15, 13, 35, 44, 579, DateTimeKind.Utc).AddTicks(2322), "AQAAAAEAACcQAAAAEHZzbs27AKTQUlEvjc4AnAlSkehPKsdr0eScbX5RoiU+uhlpZcnQwcu1Cwpv1kownA==", "357ce597-96ec-4cbc-a74f-a92f63945e1e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadProposalIId",
                table: "Notification",
                type: "integer",
                nullable: true);

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
    }
}
