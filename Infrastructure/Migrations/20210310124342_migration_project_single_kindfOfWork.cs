using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class migration_project_single_kindfOfWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KindOfWorkProject");

            migrationBuilder.AddColumn<int>(
                name: "KindOfWorkId",
                table: "Project",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ef455fd0-da3c-4544-b39e-cb7470aab258");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "319d0bf2-900f-4682-a223-18bf929337a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "15fed5e5-d6a2-4392-929e-226da7fb581c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c991223e-55cd-45b8-bd79-b6ec185daa16", "AQAAAAEAACcQAAAAEFPPvUDj9xZ2gkXHCOyuE463QTC+haKTX913zhQpo453JhjgDsvpgQhLnOPQMLG9qQ==", "5b27c094-743b-4fb3-a92a-69d6a30ef8e1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "151e0977-7721-4334-90fa-8abc523d8b6d", "AQAAAAEAACcQAAAAEChQy63m827niLDWItU9yLhkh9i6LMVbA2O5BV+4jVtJeZ66DLw7HJ3OASEVPVJ5aA==", "e9ec1dbb-4126-4ba6-9790-089159b38554" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Project_KindOfWorkId",
                table: "Project",
                column: "KindOfWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_KindOfWork_KindOfWorkId",
                table: "Project",
                column: "KindOfWorkId",
                principalTable: "KindOfWork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_KindOfWork_KindOfWorkId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_KindOfWorkId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "KindOfWorkId",
                table: "Project");

            migrationBuilder.CreateTable(
                name: "KindOfWorkProject",
                columns: table => new
                {
                    KindOfWorksId = table.Column<int>(type: "integer", nullable: false),
                    ProjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KindOfWorkProject", x => new { x.KindOfWorksId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_KindOfWorkProject_KindOfWork_KindOfWorksId",
                        column: x => x.KindOfWorksId,
                        principalTable: "KindOfWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KindOfWorkProject_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e4efcc24-9714-46d1-b71f-d6478e6c5e9b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "966e3b56-0615-4a25-bc87-638bbb5fe0f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "14553bbb-9769-47ee-89e5-c06870228e12");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "987bfae7-f3d2-4430-9c4b-61e2ff6459b7", "AQAAAAEAACcQAAAAECkyj96MsoG+J9EjTHzOPXV6Cn2eKfjk0CQBvTtBtpDsKy9xUsvOMOZTkWtUBmncAA==", "80730624-5495-4705-9547-898505c22baa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f98708f2-de0d-4596-96a0-25e7bc02d928", "AQAAAAEAACcQAAAAENhRyUOSSUMvD9Pn1UeshB+QGOCaELFcXSNc2Z1gwqLUtMI+xZ4UsR6QYtsd85E34g==", "5fa8d778-abc9-4e71-a06a-70ffaca0ad38" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_KindOfWorkProject_ProjectsId",
                table: "KindOfWorkProject",
                column: "ProjectsId");
        }
    }
}
