using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_ReceiverUserId",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "ReceiverUserId",
                table: "Notification",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_ReceiverUserId",
                table: "Notification",
                newName: "IX_Notification_ReceiverId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_ReceiverId",
                table: "Notification",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_ReceiverId",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Notification",
                newName: "ReceiverUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_ReceiverId",
                table: "Notification",
                newName: "IX_Notification_ReceiverUserId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b791a733-13e1-427d-81cb-655a058aa6ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9c2a0f96-31f2-4edd-9ca8-8321d732135a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "fa5a1b9a-4b11-4e52-b966-8e3b509871fa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed8c5450-799e-4b09-9de2-b5caec05e07b", new DateTime(2021, 5, 13, 11, 12, 3, 536, DateTimeKind.Utc).AddTicks(7159), "AQAAAAEAACcQAAAAEKylfqkKNWBQHq+OfqhjCvbWfrYT/XdJ/XirYNFx935Wyt+bYpKbG2+VpixUio29eA==", "1fac6b57-5c7e-4992-bf7d-3a3b2e86193d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88033c05-2273-4b1a-a7b6-3eae460af4fc", new DateTime(2021, 5, 13, 11, 12, 3, 546, DateTimeKind.Utc).AddTicks(4636), "AQAAAAEAACcQAAAAEBVoExh2IHKMBEsuSC6uzIBFql0tSbjdiEjaErgvYbT7Ty7SlO7RjPhEwsjBOHGjLQ==", "3eada416-78fd-4689-a54a-a521231e5ec4" });

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_ReceiverUserId",
                table: "Notification",
                column: "ReceiverUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
