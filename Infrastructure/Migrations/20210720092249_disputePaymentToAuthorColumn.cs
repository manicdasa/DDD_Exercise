using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class disputePaymentToAuthorColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaymentToAuthor",
                table: "Dispute",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "28f61427-ef67-4a28-bb35-496775585c93");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c8d91a1b-0d63-438e-bc12-c4d9c3106948");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5afc6b0a-c9eb-4158-9522-a2160daada34");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9e96d3b-fc25-418b-91fb-bc997373ec78", new DateTime(2021, 7, 20, 9, 22, 48, 115, DateTimeKind.Utc).AddTicks(2808), "AQAAAAEAACcQAAAAEG7/tSaiCBzkF0wGCkztUpBYJYzUxeHDvK6Tg0U1F7GMsFV2Is9D8UhlkZ44fx/zpw==", "10627e8a-713c-4dcd-ab97-f445adf192c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66fdd4a8-5a6f-4870-ae48-f8d3e3332485", new DateTime(2021, 7, 20, 9, 22, 48, 116, DateTimeKind.Utc).AddTicks(9286), "AQAAAAEAACcQAAAAEKXLN/ByrTexwi1uh13NsvoBXhedcSDeyAfJPNo9whxINJPZFF5Gnpv2Fb2d8d/+Tg==", "96c17f26-e949-4ee3-8a4f-2c7d946e1c4e" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentToAuthor",
                table: "Dispute");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9a569851-0e97-414e-a01e-74c7aa8d2fbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9daf5e61-7ae7-49a8-95d7-3586e4d663b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a73d57ac-ec3b-4f77-850b-89ce31785c1d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "95123b15-0280-4381-a8a3-1f950e1cfe18", new DateTime(2021, 6, 28, 15, 48, 5, 183, DateTimeKind.Utc).AddTicks(7233), "AQAAAAEAACcQAAAAEGG7VLj6FVKf30R8teZUhjESYdNJh+xTZThJvKjY26lr+fymq5/8WA3wp0aer5zz8g==", "ddea5c67-82d6-4321-a0e9-b10a6f0b23e1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "049ac095-be2a-4199-9971-dad51f3e17ec", new DateTime(2021, 6, 28, 15, 48, 5, 191, DateTimeKind.Utc).AddTicks(6309), "AQAAAAEAACcQAAAAEHZdlW1qN2DcFn1EXkFrGgYfR+PISdRSxHOohFsXzOPb6HR7+ylWKRLVQ4Iv2aTL7Q==", "0510603a-8edb-4fa0-9639-cee212d53d00" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 6, 28, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
