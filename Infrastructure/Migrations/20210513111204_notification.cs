using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: true),
                    DetailsLink = table.Column<string>(type: "text", nullable: true),
                    IsSeen = table.Column<bool>(type: "boolean", nullable: false),
                    DateTimeCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NotificationType = table.Column<int>(type: "integer", nullable: false),
                    ReceiverUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ReceiverUserId",
                table: "Notification",
                column: "ReceiverUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ab896a2a-4db2-49fe-b3e4-c127eaab2de9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "80ab5b4c-a155-4455-a170-fb87782bb23d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "20b153c1-7c08-4d4e-bba9-75e8415f66b1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57bc0268-8078-4fa7-806b-2aa79b8d92ac", new DateTime(2021, 5, 10, 14, 57, 32, 805, DateTimeKind.Utc).AddTicks(9202), "AQAAAAEAACcQAAAAEJgOMiMaqPWAWXBws6nD7BXXmyuNPFTCn0wG3taQUKZvaiJh44s/30ldH2tL+mxG9A==", "c5e4c779-61e1-4f83-b67f-898e8b59ef65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "336d05ec-f91d-4626-af4c-9a08f89571d4", new DateTime(2021, 5, 10, 14, 57, 32, 813, DateTimeKind.Utc).AddTicks(3903), "AQAAAAEAACcQAAAAENMec47yP5JSKqc0U6/oowlkT/FusbfQx1Jqq/JW9NrfbiIgJ8pe0UB+yRpGJBQMeA==", "4dc6ed58-ef88-4ccd-9f8f-4a168f610030" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
