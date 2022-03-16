using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class booking_status_history_added1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "36413ede-30c5-4133-b89c-9ca721b0afe1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "041c0d01-ee89-494c-b11e-c761e097e264");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "607c525b-d1ad-4241-9ff9-fc283dbbe0cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5270c1df-41d4-48d9-b9a5-2c755e7b4770", "AQAAAAEAACcQAAAAENKUsa9SVSjEPTi/xAFoyxsJW5Nm2L/Y5br64OwyRC09+ZDF8WAQGn1PPWcLMRkjcA==", "a58d9bbd-f590-4b07-ae84-cfe97d9e0b7b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37e3ef53-bac8-480f-a487-7e41bdb17378", "AQAAAAEAACcQAAAAEIvhh5+rqP8s3DVIBiWnp4nM+/LmaYxMAB+gVixa91ojt8Uu8qOX7OQ2p2j0RCQhNQ==", "4d38d8c2-89f5-4046-a66c-0e17d8e043ed" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3c2deb18-1a94-4bf8-8441-e29c0006ab43");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a6042eb0-b2db-44ec-8b8d-675dda47d4a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f99c8990-78f0-420a-bee6-6469476606ad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e57e7b56-08f1-4b0e-a3f2-8d25efbe4a22", "AQAAAAEAACcQAAAAEEur258Plx0A8DRt+E/aHmwdTlpz/jRUvN2u9yPhM/Ea4iQUuo6l9ixpTaBOLiNMMA==", "8020da70-2b95-44ae-9706-d4b3806caa8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7deb469-4ee9-4c26-962a-1f0e3225bdd3", "AQAAAAEAACcQAAAAEGLciWyTnVFJo4MqWKS+Lcptc1T8PYz3IdNJoHCm8gDrSqVNqV54HzbzOmO5ypD4yg==", "ec3fb854-7f22-4f6e-a850-a99830173394" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 19, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
