using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification_update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorUsername",
                table: "Notification",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerUsername",
                table: "Notification",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Notification",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTopic",
                table: "Notification",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "77f4cd02-3ce7-4e28-bb97-35f70f991905");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0e999d7c-4552-4c2b-ac77-022bfcfc95c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1f7d9b21-e3ee-46eb-88f7-efae92c95330");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e61e9054-d8be-473f-90d4-424d761cdaa2", new DateTime(2021, 6, 15, 13, 29, 43, 369, DateTimeKind.Utc).AddTicks(5202), "AQAAAAEAACcQAAAAEH+9uPRto7JZ06m+k8hQgbKR/T9f0+H8V+lGW+i0nB4TLs5CMpmaYo7qK712COO6pg==", "656e8f42-283c-4ad2-8245-c2831152c8ff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9e133eb-3583-4c99-986b-be812656eec9", new DateTime(2021, 6, 15, 13, 29, 43, 377, DateTimeKind.Utc).AddTicks(5800), "AQAAAAEAACcQAAAAEM8A2bzuX3Q+5SJK189omQnI1lBLePsRPaFax74tivERzgVdrOS17A4NoATL87B3sw==", "645b5c49-0e75-4bea-83c3-65be622aa4f9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorUsername",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CustomerUsername",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ProjectTopic",
                table: "Notification");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6488c75f-c6fc-4698-a332-b24e93c7c49b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cc7c9b69-df24-4850-a0d0-ac5fd790f4c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0c81d91b-0b60-41a0-b66c-0f24d74481be");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "638c2690-4cc7-40bc-b4d9-c0a695c65b23", new DateTime(2021, 6, 15, 8, 46, 58, 255, DateTimeKind.Utc).AddTicks(5145), "AQAAAAEAACcQAAAAEKd0SZ3guhZ0zc2S9mkuXoO0s7ygxLU+eQiLUeK4JEtSxYvtNEftdFpP/838apAMaw==", "a073dcc0-468c-43f5-8485-ccb5bad84919" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfc711e1-114c-4630-85b4-2f0056fd3f13", new DateTime(2021, 6, 15, 8, 46, 58, 270, DateTimeKind.Utc).AddTicks(2772), "AQAAAAEAACcQAAAAEHBvlDY2sb+rqnzZ4mh2wzb7yAL8ykDVkJ1a34ykiUki+mXf76nV+Uyk6PkDpDq8Wg==", "e9831b18-3795-4fa1-a9ba-ab3fcff98b09" });
        }
    }
}
