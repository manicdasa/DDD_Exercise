using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class booking_status_history_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookingStatus = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BookingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingStatusHistory_Booking_BookingId",
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

            migrationBuilder.CreateIndex(
                name: "IX_BookingStatusHistory_BookingId",
                table: "BookingStatusHistory",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingStatusHistory");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b02d1926-02cf-4904-96f6-e03894f11cb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "67b9375a-409f-44ef-bebc-ab49a6394e6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ef006aae-2aa2-47d1-9cac-4cf8d2f9bc4b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "945db8e3-7e47-4a24-8ede-4375979da260", "AQAAAAEAACcQAAAAELTMoij129X0i+URf8AOTSzeIMqErUFv75mzlwRMDU6IWzcEhR4APMWqAHm1Q5l7sw==", "c17a9449-6773-4a42-9db5-6dd31c6b6b4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d33da33-e502-4e46-972b-bbfd312ff1de", "AQAAAAEAACcQAAAAEOUDf5L34dNNsmDGw4FLSPJcoIt4VnQBpX+Fwq3aIA59WDfh7LsGYkAdZNTUM3lfAQ==", "dcb1bc3e-7500-4b58-801f-f2de4bbe5e12" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
