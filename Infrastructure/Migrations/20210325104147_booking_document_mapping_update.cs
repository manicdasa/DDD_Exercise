using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class booking_document_mapping_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RealName",
                table: "Document",
                newName: "PrivateName");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrivateName",
                table: "Document",
                newName: "RealName");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "de276ba5-6791-4365-a481-e2390c7656db");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "36f1a284-afec-45d8-9111-58fd4f9dc8f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "779b1b33-dc96-4e8d-83ff-780bb217874d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90bc4415-ae36-4880-b931-669ea663dd68", "AQAAAAEAACcQAAAAEH6gr5pujaVYCMfz4Cm+TQON5bMK1iOIojxuu9lo8wGc3bONC86AhKYiQGQ54jv0Kw==", "b3c1442c-1be8-4ab1-8258-65d3c89aa678" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3b73c6e-3efc-457d-817f-5fe281b75288", "AQAAAAEAACcQAAAAEKRmM61EvPYocb1/DLiFQ30ei0bC576NH82CA5vX2rf2dHLMcoSEOYnG5DK7B+lp+g==", "b38f3dd5-df70-41b8-a349-36ec0a5d1305" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 24, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
