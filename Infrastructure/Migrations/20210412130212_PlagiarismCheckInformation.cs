using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class PlagiarismCheckInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlagiarismCheckInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdenticalWords = table.Column<long>(type: "bigint", nullable: false),
                    MinorChangedWords = table.Column<long>(type: "bigint", nullable: false),
                    RelatedMeaningWords = table.Column<long>(type: "bigint", nullable: false),
                    AggregatedScore = table.Column<double>(type: "double precision", nullable: false),
                    Credits = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<int>(type: "integer", nullable: false),
                    DownloadEndPointUri = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlagiarismCheckInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlagiarismCheckInformation_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "77d22ab2-9497-4767-b630-8ae4b6aad2bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5701d385-3e71-46fc-9fc5-f63c881b04a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b13be0f6-0dd2-4381-a9c4-2933c4be557e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3aecc43-9853-4e34-b8a0-543fdb922802", new DateTime(2021, 4, 12, 13, 2, 11, 103, DateTimeKind.Utc).AddTicks(8347), "AQAAAAEAACcQAAAAEC5nHY3vtzTKKe16Qxa5zZ4+QUPIHgJEnLKH/Tf2sX9qS9ZPffwp0nO8z67dqh+DNA==", "3bf1c9d5-3c86-4b17-97bf-cdd86e2fe986" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "453b57cd-2c96-43a8-b911-c8be1b907f25", new DateTime(2021, 4, 12, 13, 2, 11, 112, DateTimeKind.Utc).AddTicks(2767), "AQAAAAEAACcQAAAAEFb4OI33TeQElcL27yL/bEjRmIazSa6amu1HnnpqhFUfUv29tCMKmWQ/D8cy/DY/FQ==", "974b2bf4-e7ee-4d13-82e3-f47d83af29a8" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_PlagiarismCheckInformation_DocumentId",
                table: "PlagiarismCheckInformation",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlagiarismCheckInformation");

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
    }
}
