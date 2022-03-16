using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class transaction_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillingAddress",
                table: "Transaction",
                newName: "TransactionMessage");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6752bb1e-bb95-48fc-8799-9f45d396d294");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "30462d73-f717-4e2b-9fd5-fbe0c851a392");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7779d834-77b5-4288-a1d0-39506a66a16e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbff5fea-d8cd-40d6-99f0-56c4ab0a779c", new DateTime(2021, 5, 18, 10, 56, 34, 924, DateTimeKind.Utc).AddTicks(4386), "AQAAAAEAACcQAAAAEExZ/IuB8Q93oLykR4vq2rMSGH/kaqlMKTe8iY3b8wKHngjA/3QyLF6FwZIrOMsQtw==", "892c5125-eb95-4a08-9c5a-1bb293159fee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18ed3126-170f-4456-98c5-f9ea3fd417c8", new DateTime(2021, 5, 18, 10, 56, 34, 931, DateTimeKind.Utc).AddTicks(8160), "AQAAAAEAACcQAAAAEOpInK3JkvT7IfZos+AXj7tIOw37JkYNYjwWJwhLmIx710V/FaHRMGx19/mz67PTMg==", "c13f0dfa-4ff5-4ed9-9503-8fea4fd11bc3" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionMessage",
                table: "Transaction",
                newName: "BillingAddress");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3961b3fa-0713-4657-8ac6-696e608b6385");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8619cde0-df69-472c-8dfb-3562b6202825");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9a587081-09a2-4e49-a963-629dc6fe17aa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f66040fd-4e79-4149-bc42-8dab7ff64a29", new DateTime(2021, 5, 13, 14, 2, 39, 578, DateTimeKind.Utc).AddTicks(7453), "AQAAAAEAACcQAAAAEA0i/iZdHuNwrL/5np8SyJo8ODFnmDW5R0cZ2I76ctj1lhq2PHK04NnG1ECtqhBTow==", "ebdf9576-49fa-4281-a10b-1a8c53270b95" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d620784d-ebfe-4112-9ec8-c1b0e929808f", new DateTime(2021, 5, 13, 14, 2, 39, 587, DateTimeKind.Utc).AddTicks(4021), "AQAAAAEAACcQAAAAEMfXWS+3/UkMFozkJFZVt7+gnqlcqDztgs4JC1rV9w6XcH5j4y7xGTOedcHauupfMw==", "10d916e2-5230-4330-9a80-2bb74066d981" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
