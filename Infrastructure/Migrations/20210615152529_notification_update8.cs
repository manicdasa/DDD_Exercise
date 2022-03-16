using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification_update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadProposalId",
                table: "Notification",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProposalId",
                table: "Notification",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4273975a-02f5-4c9d-8a50-8f1d09590d44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "50f2e62e-765d-4f6f-9855-169031d19291");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "58e32269-8e3b-4eec-b53e-f4c3a4e07178");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69c46dfb-cc69-4062-972a-7d97ff75bd38", new DateTime(2021, 6, 15, 15, 25, 28, 221, DateTimeKind.Utc).AddTicks(2453), "AQAAAAEAACcQAAAAEAilj0ORdovNxUaxXYYAMEjhxDQfKeauk9cdXEKID6aewOxVSNncXzKR3XuFgNJTPw==", "3e9d65ac-e758-4b0a-942d-158c4b7ef7d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed86806d-344b-4693-9619-11f0ae60c553", new DateTime(2021, 6, 15, 15, 25, 28, 229, DateTimeKind.Utc).AddTicks(3699), "AQAAAAEAACcQAAAAEGQtr1WSAvodEkeWC40qTvroBlXQjd0xyWRsxmuzgVAOdOu7yh559OZOy3nyyEpapg==", "8ddab338-8320-462b-bd12-6ca6f3b22856" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadProposalId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ProposalId",
                table: "Notification");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b8a129f1-29fb-4c2c-b988-6965e896b863");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1c9f1f21-40cc-450a-ac2a-1ce7261b5951");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "68db1581-e7e3-448a-995d-5ee30218e13b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9599ba3d-523a-40bf-9221-07ed1da7ba9d", new DateTime(2021, 6, 15, 13, 58, 16, 258, DateTimeKind.Utc).AddTicks(7285), "AQAAAAEAACcQAAAAED6+JGtvhdrKD6sSOqNogTrUkTSipET5YCwT1dTUg+bR2mQ3Z3IkUe6aY4WAOj1mTg==", "368dc31c-9ce2-4b37-a085-bd35e2a90c7c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07a0406d-5000-44f4-b6da-cf0184326a72", new DateTime(2021, 6, 15, 13, 58, 16, 266, DateTimeKind.Utc).AddTicks(3525), "AQAAAAEAACcQAAAAECJDnB3hXutDubfWDhOlJQqEPx89xXRsELlQdEEU5+dCJyBCjV0mq/FvQPvzL6DS6Q==", "eb75a7e6-88e1-421d-b866-fd44586e8b59" });
        }
    }
}
