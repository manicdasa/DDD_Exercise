using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class kindOfWorkSeeding4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "16c48f53-a963-4157-91f0-b0ae53f862e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "09b845e4-a37c-4b43-99f0-b452e9109105");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7283e558-7213-4d25-9298-d418a08be8ff");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abd6fc31-3522-467f-a68b-052876e4632c", new DateTime(2021, 6, 28, 15, 36, 17, 131, DateTimeKind.Utc).AddTicks(960), "AQAAAAEAACcQAAAAEFeKGozGL08oE0C/o52RpLen2qSX2HMvAGqp5g8DkXzfz0bRZHcdYsPehrBf6VgQMw==", "c8347776-330f-45de-bb13-e55afd1c0c58" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea25159e-4454-42b3-85cc-1fb4d5928450", new DateTime(2021, 6, 28, 15, 36, 17, 138, DateTimeKind.Utc).AddTicks(6800), "AQAAAAEAACcQAAAAEFNhxMMSTNYkyIwBPcga6NWGb53CvGs8kvG5HzpAF1bB6BJc2J4JF+vg61akPdNn2w==", "ffdcf62a-769d-4d48-826b-360791802a48" });

            migrationBuilder.InsertData(
                table: "Degree",
                columns: new[] { "Id", "Description", "Stage", "Value" },
                values: new object[,]
                {
                    { 24, "Fakultätsexamen", 1, "Fakultätsexamen" },
                    { 23, "Magister", 4, "Magister" },
                    { 22, "Prof.", 4, "Prof." },
                    { 21, "Dr.", 4, "Dr." },
                    { 20, "Habilitation", 4, "Habilitation" },
                    { 19, "Master of Business Administration (MBA)", 3, "Master of Business Administration (MBA)" },
                    { 18, "Master of Laws (LL.M.)", 3, "Master of Laws (LL.M.)" },
                    { 17, "Master of Science (M.Sc.)", 3, "Master of Science (M.Sc.)" },
                    { 16, "Master of Education (M.Ed.)", 3, "Master of Education (M.Ed.)" },
                    { 14, "Diplom (FH)", 2, "Diplom (FH)" },
                    { 13, "Diplom (Uni)", 2, "Diplom (Uni)" },
                    { 12, "Bachelor sonstiges•Master of Arts (M.A.)", 2, "Bachelor sonstiges•Master of Arts (M.A.)" },
                    { 11, "Bachelor of Science in Information Technology (B.Sc.IT)", 2, "Bachelor of Science in Information Technology (B.Sc.IT)" },
                    { 10, "Bachelor of Laws (LL.B.)", 2, "Bachelor of Laws (LL.B.)" },
                    { 9, "Bachelor of Science (B.Sc.)", 2, "Bachelor of Science (B.Sc.)" },
                    { 8, "Bachelor of Education (B.Ed.)", 2, "Bachelor of Education (B.Ed.)" },
                    { 7, "Bachelor of Engineering (B.Eng.)", 2, "Bachelor of Engineering (B.Eng.)" },
                    { 6, "Bachelor of Arts (B.A.)", 2, "Bachelor of Arts (B.A.)" },
                    { 5, "Kirchlicher Abschluss", 1, "Kirchlicher Abschluss" },
                    { 4, "Hausarbeit", 1, "Hausarbeit" },
                    { 15, "Master of Engineering (M.Eng.)", 3, "Master of Engineering (M.Eng.)" },
                    { 3, "Kein Abschluss/Nicht festgelegt", 1, "Kein Abschluss/Nicht festgelegt" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e61380eb-7be1-406e-9a87-7355cb05006f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a8dd753a-9090-41a9-8873-397b5a8d05c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "bfabf201-0b2c-4fe9-a05e-7983e9f24e3f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56246ea6-d7c2-45d6-aa81-ebb790db04b7", new DateTime(2021, 6, 28, 14, 59, 17, 391, DateTimeKind.Utc).AddTicks(3379), "AQAAAAEAACcQAAAAEM8/S5HLDf3WOwyqFo6fOK4uRzYUHDAkvFYzwsuJkgR+yudQLsDaWRutXe5IvIeXkQ==", "77201612-2849-4372-b835-22ee9796a971" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd9103b8-6adc-42cd-9ad9-932b4a33014b", new DateTime(2021, 6, 28, 14, 59, 17, 398, DateTimeKind.Utc).AddTicks(9384), "AQAAAAEAACcQAAAAEMAuvCau2aSGbN4gJlShKRxr4y2BT0rASzWqz7eYJRgBqMcWMZ0pKp1sauE/vj9ssQ==", "ecdc1061-09fe-406d-bb33-5c859fc6335a" });
        }
    }
}
