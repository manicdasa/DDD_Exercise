using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class paymentVerified_cln_added_tbl_UserRoleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PaymentVerified",
                table: "UserRoleData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8f18ac0e-bfb1-4b71-b6f4-64a2fd586a86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3014a2f9-a0f9-4b31-9e44-240b6a7ef6d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ece5fae1-f976-458b-ae3b-f94edc021c93");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b56a41f-a751-4aef-9056-83b71363e35f", new DateTime(2021, 4, 9, 10, 8, 38, 775, DateTimeKind.Utc).AddTicks(1983), "AQAAAAEAACcQAAAAEDVOQiuJb12TeofhNUEK2jB5jSUB4+RJZkTchsujq+uWNuIYvCUqJW8x9JL5siywzg==", "527eb072-06b9-4873-8f4a-f9ba45df94e3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5510fd4c-1171-4878-a761-2d9de4406a5d", new DateTime(2021, 4, 9, 10, 8, 38, 783, DateTimeKind.Utc).AddTicks(5282), "AQAAAAEAACcQAAAAEElHHPf7qFVJo3w0R46AbXdWU+fvLc1RNJjjmjkngbCDoB3M9nUMC3ex33U0kLxZzA==", "a014e83d-082e-45af-8343-7337fe2a04fc" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentVerified",
                table: "UserRoleData");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8b794c0e-a37c-47b3-a6eb-eee845cf4aec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4a39835a-4bfb-4d67-b320-67f46c1a414f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "72b60881-2b43-4417-8d1e-3a5ede8771c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ecd8cea4-9e2c-445a-bfaf-5cadaa0cbafc", new DateTime(2021, 4, 5, 13, 4, 49, 772, DateTimeKind.Utc).AddTicks(7230), "AQAAAAEAACcQAAAAEIKxzN/c0IY3Slten0GMTv+r2DSYewypgc3TaUKH9oJcCtIedQFWVJJioF7NtaUUVA==", "9273ffc5-a768-460f-9494-5d0f0347eb5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24fd736e-9a1a-43b9-9d5a-bc6fefef9e94", new DateTime(2021, 4, 5, 13, 4, 49, 781, DateTimeKind.Utc).AddTicks(45), "AQAAAAEAACcQAAAAEMuHs0ON0aU7Uj+Wve18ByHZKRZ94Ddlaq3s9k2QI1ItDJQTtwBjRnyXik2Gwa96KQ==", "0f97b7dc-fc08-4f66-b407-2c8fa5c94f93" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
