using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class booking_transaction_mapping_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.RenameColumn(
                name: "FinancialOfferWithCharges",
                table: "Proposal",
                newName: "ServiceCharges");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: true),
                    PaymentMethodNonce = table.Column<string>(type: "text", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "boolean", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    BookingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8427c3e4-7835-4590-888a-234b82af484d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6c0aea5a-fdf9-4c96-820c-2b848abae1a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a5a9fe79-08a9-4023-83cf-3b5aca17a08d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da3737e8-37c4-49b4-836d-82d58dcb95d1", "AQAAAAEAACcQAAAAEFO34g/bxk8pDc1g09PLe3ruXfFGaNT4VaiDqOc74SSB8zU59auXD2F3iSiZpZqL9Q==", "25b2ab16-9937-4dbc-9a64-cb73d87f0ba3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "861715f4-d9f3-4f1c-ae6c-f48b1a3185c9", "AQAAAAEAACcQAAAAEEJ92ckGcbjdlKiHsFRn1pS4q6TtOIvlL5mtcYHUO3YXWk5E/PZAL36hQknaclu2aA==", "824806dc-faf5-45b1-9c66-cf45f40e657e" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 26, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BookingId",
                table: "Transaction",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.RenameColumn(
                name: "ServiceCharges",
                table: "Proposal",
                newName: "FinancialOfferWithCharges");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookingId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    PaymentCharges = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e18ad27e-9a2b-4823-b9fd-886b3643cb77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8dfba205-f1cf-4d37-99bc-ab0097f514d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "cfdf4e3c-4016-473c-9abf-a3b0c9c1abd9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fd77892-c8da-4da8-af4c-a69106661a48", "AQAAAAEAACcQAAAAEMuBKeK4Hi6xMg0YgEYNhEG/DOBFAC2VM3AKCAJd1JBfwg1agmQI5ONpZHgHlnow+w==", "caf389c6-39ff-48f5-bcec-cc37ee62fe50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbeb82a5-347b-4796-baff-1bff8124d6fa", "AQAAAAEAACcQAAAAEIVPzX5ONW/1j93bZbQnqt3Kbwho+xsqj3H1IEY3wwzmWFwwvfXNshFjdVpolITlFg==", "af98561e-d93c-4b88-b196-bf5d8f165f2b" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BookingId",
                table: "Payment",
                column: "BookingId");
        }
    }
}
