using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class kindOfWorkSeeding3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "ExpertiseArea",
                columns: new[] { "Id", "Description", "FieldStatus", "Value" },
                values: new object[,]
                {
                    { 120, "Datenschutzrecht", 1, "Datenschutzrecht" },
                    { 91, "Pharmazie", 1, "Pharmazie" },
                    { 92, "Pharmatechnik", 1, "Pharmatechnik" },
                    { 93, "Zahnmedizin", 1, "Zahnmedizin" },
                    { 94, "Tiermedizin", 1, "Tiermedizin" },
                    { 95, "Agrarwissenschaft", 1, "Agrarwissenschaft" },
                    { 96, "Forstwissenschaft", 1, "Forstwissenschaft" },
                    { 97, "Gartenbau", 1, "Gartenbau" },
                    { 98, "Holzwirtschaft", 1, "Holzwirtschaft" },
                    { 99, "Holztechnik", 1, "Holztechnik" },
                    { 100, "Landschaftsarchitektur", 1, "Landschaftsarchitektur" },
                    { 101, "Landwirtschaft", 1, "Landwirtschaft" },
                    { 102, "Betriebswirtschaft", 1, "Betriebswirtschaft" },
                    { 103, "Volkswirtschaft", 1, "Volkswirtschaft" },
                    { 104, "Wirtschaftspädagogik", 1, "Wirtschaftspädagogik" },
                    { 105, "Bauingenieurwesen", 1, "Bauingenieurwesen" },
                    { 106, "Drucktechnik", 1, "Drucktechnik" },
                    { 90, "Gesundheitswissenschaften", 1, "Gesundheitswissenschaften" },
                    { 108, "Informationstechnik", 1, "Informationstechnik" },
                    { 109, "Lebensmitteltechnologie", 1, "Lebensmitteltechnologie" },
                    { 110, "Maschinenbau", 1, "Maschinenbau" },
                    { 111, "Medientechnik", 1, "Medientechnik" },
                    { 112, "Raumplanung", 1, "Raumplanung" },
                    { 113, "Umweltschutz", 1, "Umweltschutz" },
                    { 114, "Vermessungswesen", 1, "Vermessungswesen" },
                    { 115, "Materialwissenschaften", 1, "Materialwissenschaften" },
                    { 116, "Jura", 1, "Jura" },
                    { 117, "Medienrecht", 1, "Medienrecht" },
                    { 118, "Wirtschaftsrecht", 1, "Wirtschaftsrecht" },
                    { 119, "Strafrecht", 1, "Strafrecht" },
                    { 121, "Öffentliches Recht", 1, "Öffentliches Recht" },
                    { 107, "Elektrotechnik", 1, "Elektrotechnik" },
                    { 89, "Medizin", 1, "Medizin" },
                    { 87, "Informatik", 1, "Informatik" },
                    { 66, "Kulturwissenschaft", 1, "Kulturwissenschaft" },
                    { 65, "Kommunikationswissenschaft", 1, "Kommunikationswissenschaft" },
                    { 64, "Journalismus", 1, "Journalismus" },
                    { 63, "Architektur", 1, "Architektur" },
                    { 62, "Gestaltung", 1, "Gestaltung" },
                    { 61, "Geschichte", 1, "Geschichte" },
                    { 60, "Germanistik", 1, "Germanistik" },
                    { 59, "Design", 1, "Design" },
                    { 58, "Bibliothekswesen", 1, "Bibliothekswesen" },
                    { 57, "Theologie", 1, "Theologie" },
                    { 56, "Sport", 1, "Sport" },
                    { 55, "Soziologie", 1, "Soziologie" },
                    { 54, "Wirtschaftspsychologie", 1, "Wirtschaftspsychologie" },
                    { 53, "Psychologie", 1, "Psychologie" },
                    { 52, "Politikwissenschaft", 1, "Politikwissenschaft" },
                    { 67, "Kunstgeschichte", 1, "Kunstgeschichte" },
                    { 88, "Haushalt", 1, "Haushalt" },
                    { 68, "Medienwissenschaft", 1, "Medienwissenschaft" },
                    { 70, "Philologie", 1, "Philologie" },
                    { 86, "Verfahrenstechnik", 1, "Verfahrenstechnik" },
                    { 85, "Mathematik", 1, "Mathematik" },
                    { 84, "Statistik", 1, "Statistik" },
                    { 83, "Physik", 1, "Physik" },
                    { 82, "Geologie", 1, "Geologie" },
                    { 81, "Geografie", 1, "Geografie" },
                    { 80, "Ernährungswissenschaften", 1, "Ernährungswissenschaften" },
                    { 79, "Chemie", 1, "Chemie" },
                    { 78, "Biologie", 1, "Biologie" },
                    { 77, "Biochemie", 1, "Biochemie" },
                    { 76, "Theaterwissenschaft", 1, "Theaterwissenschaft" },
                    { 75, "Sprachwissenschaften", 1, "Sprachwissenschaften" },
                    { 74, "Religion", 1, "Religion" },
                    { 73, "Publizistik", 1, "Publizistik" },
                    { 72, "Philosophie", 1, "Philosophie" },
                    { 69, "Musik", 1, "Musik" },
                    { 51, "Erziehungswissenschaften/Pädagogik", 1, "Erziehungswissenschaften/Pädagogik" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "298dac97-c4da-4f01-bc20-c68f07d72977");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c359d7a4-4345-4426-acc2-ece5e2265297");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e194d355-1da9-4c6f-84aa-66f9d7c0123d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38b2088e-2596-44c8-9bb4-27b05bff0eaf", new DateTime(2021, 6, 28, 14, 42, 53, 900, DateTimeKind.Utc).AddTicks(3880), "AQAAAAEAACcQAAAAEI/UNnKWZTcXXrh7qoKp8oNjNsw9LIIP/v+VboBXSjLtJFDYKzhgGkayEIwIvGHhpw==", "b7775384-e72e-4207-bb61-0666b79440e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "988e9b4c-6614-4fe3-bce3-32dec13c92ee", new DateTime(2021, 6, 28, 14, 42, 53, 910, DateTimeKind.Utc).AddTicks(8487), "AQAAAAEAACcQAAAAEAEbeDc7/v6un/AZv6G04dVP0orYxVF9c00OByvqyFfVz7UdxEMGFp6tqVwmD2dLwg==", "5c6bf348-dc27-474c-9080-b190678ab901" });
        }
    }
}
