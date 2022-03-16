using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification_update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9119151a-0976-4179-8e4d-5d04875efb64");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9877c804-5dac-4eb2-800f-27320b0078c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d9dcfddf-1939-490c-bf48-7d3104b0a44d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "463b37eb-f52a-4232-930d-e2bf2f16c036", new DateTime(2021, 6, 15, 13, 35, 44, 570, DateTimeKind.Utc).AddTicks(2605), "AQAAAAEAACcQAAAAEFt1ZjLH0NmfV7m7NO3Jej+G0ipWrkXKIrSK6B0wXUK2K9Qt6KE0QzTJoNvT7xtz0g==", "5f3208fb-a070-4aff-a944-e526209237d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a08d8d7b-ea1b-4d79-b622-4ca28475bd17", new DateTime(2021, 6, 15, 13, 35, 44, 579, DateTimeKind.Utc).AddTicks(2322), "AQAAAAEAACcQAAAAEHZzbs27AKTQUlEvjc4AnAlSkehPKsdr0eScbX5RoiU+uhlpZcnQwcu1Cwpv1kownA==", "357ce597-96ec-4cbc-a74f-a92f63945e1e" });
        }
    }
}
