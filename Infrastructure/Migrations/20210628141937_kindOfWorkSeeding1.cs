using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class kindOfWorkSeeding1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a07f3f76-2aec-4e84-9d18-aed34e7f6c0c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ca84e8d5-c30d-41e0-bd0c-d00760db7a75");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "263b3c6a-5644-445a-b398-125b70a1434a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93fb9863-252c-40a0-82bc-42b0f1f994bb", new DateTime(2021, 6, 28, 14, 19, 36, 589, DateTimeKind.Utc).AddTicks(763), "AQAAAAEAACcQAAAAECP4AoQqFUQSit9AnR3cvkXGH1Kyfexhbu9q8kLO2eTENlv6czDFvN+o5EwRuW4BuA==", "34481a9d-596b-4180-8eb9-6fc506dd5823" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a9d14e0-1fb8-4470-a6ae-4cc4a7b68cd9", new DateTime(2021, 6, 28, 14, 19, 36, 596, DateTimeKind.Utc).AddTicks(7922), "AQAAAAEAACcQAAAAENETh7JqvOJwsO1zl3eWYb23zCP/D2fzafi/fjU34kiVzyCnpZV4TXlqtitrSPitHQ==", "3600af05-0f9d-4e1b-b80a-8340b619226f" });

            migrationBuilder.InsertData(
                table: "KindOfWork",
                columns: new[] { "Id", "Description", "FieldStatus", "Value" },
                values: new object[] { -1, "Wissenschaftliches Paper", 1, "Wissenschaftliches Paper" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 6, 28, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KindOfWork",
                keyColumn: "Id",
                keyValue: -1);

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

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
