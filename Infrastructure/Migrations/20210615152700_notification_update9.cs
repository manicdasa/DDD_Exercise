using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification_update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b20d0399-d74a-4048-92ff-e240bb4e18c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cc8fbbec-dbf6-4b36-8bf0-e7d76268b6ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "72183084-c70d-48ec-8365-d127bfa9dbc0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba71a458-548f-4737-8803-e2d3a2db1f50", new DateTime(2021, 6, 15, 15, 26, 59, 326, DateTimeKind.Utc).AddTicks(3773), "AQAAAAEAACcQAAAAEProJEbYwiOqEHmFoaqJOe8onV3sEAYrgC/SIpTlNslLy2EgW2lsbWk/IQLHcH/nBw==", "3f0e00d8-e883-4cb2-ac1e-00f8b16e4ff4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41644cd3-73dd-43fe-9639-fad48bfc1d55", new DateTime(2021, 6, 15, 15, 26, 59, 334, DateTimeKind.Utc).AddTicks(2672), "AQAAAAEAACcQAAAAEPwZ0dCvh/bfmJL/Hf0fMGoVwP6F5cLiW5xBKDRi4pq8AFDRTUNgt45hiVs1XFPOuw==", "31561af7-e0a0-4706-9571-b381bba11bfc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4273975a-02f5-4c9d-8a50-8f1d09590d44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "50f2e62e-765d-4f6f-9855-169031d19291");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "58e32269-8e3b-4eec-b53e-f4c3a4e07178");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69c46dfb-cc69-4062-972a-7d97ff75bd38", new DateTime(2021, 6, 15, 15, 25, 28, 221, DateTimeKind.Utc).AddTicks(2453), "AQAAAAEAACcQAAAAEAilj0ORdovNxUaxXYYAMEjhxDQfKeauk9cdXEKID6aewOxVSNncXzKR3XuFgNJTPw==", "3e9d65ac-e758-4b0a-942d-158c4b7ef7d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed86806d-344b-4693-9619-11f0ae60c553", new DateTime(2021, 6, 15, 15, 25, 28, 229, DateTimeKind.Utc).AddTicks(3699), "AQAAAAEAACcQAAAAEGQtr1WSAvodEkeWC40qTvroBlXQjd0xyWRsxmuzgVAOdOu7yh559OZOy3nyyEpapg==", "8ddab338-8320-462b-bd12-6ca6f3b22856" });
        }
    }
}
