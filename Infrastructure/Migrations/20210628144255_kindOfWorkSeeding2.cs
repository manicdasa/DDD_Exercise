using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class kindOfWorkSeeding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "298dac97-c4da-4f01-bc20-c68f07d72977");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c359d7a4-4345-4426-acc2-ece5e2265297");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e194d355-1da9-4c6f-84aa-66f9d7c0123d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38b2088e-2596-44c8-9bb4-27b05bff0eaf", new DateTime(2021, 6, 28, 14, 42, 53, 900, DateTimeKind.Utc).AddTicks(3880), "AQAAAAEAACcQAAAAEI/UNnKWZTcXXrh7qoKp8oNjNsw9LIIP/v+VboBXSjLtJFDYKzhgGkayEIwIvGHhpw==", "b7775384-e72e-4207-bb61-0666b79440e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "988e9b4c-6614-4fe3-bce3-32dec13c92ee", new DateTime(2021, 6, 28, 14, 42, 53, 910, DateTimeKind.Utc).AddTicks(8487), "AQAAAAEAACcQAAAAEAEbeDc7/v6un/AZv6G04dVP0orYxVF9c00OByvqyFfVz7UdxEMGFp6tqVwmD2dLwg==", "5c6bf348-dc27-474c-9080-b190678ab901" });

            migrationBuilder.InsertData(
                table: "KindOfWork",
                columns: new[] { "Id", "Description", "FieldStatus", "Value" },
                values: new object[,]
                {
                    { 44, "Wissenschaftliches Paper", 1, "Wissenschaftliches Paper" },
                    { 45, "Hausarbeit", 1, "Hausarbeit" },
                    { 46, "Bachelorarbeit", 1, "Bachelorarbeit" },
                    { 47, "Diplomarbeit", 1, "Diplomarbeit" },
                    { 48, "Habilitation", 1, "Habilitation" },
                    { 49, "Habilitation", 1, "Habilitation" },
                    { 50, "Doktorarbeit", 1, "Doktorarbeit" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KindOfWork",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "KindOfWork",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "KindOfWork",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "KindOfWork",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "KindOfWork",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "KindOfWork",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "KindOfWork",
                keyColumn: "Id",
                keyValue: 50);

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
        }
    }
}
