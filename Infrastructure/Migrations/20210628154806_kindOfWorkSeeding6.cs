using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class kindOfWorkSeeding6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9a569851-0e97-414e-a01e-74c7aa8d2fbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9daf5e61-7ae7-49a8-95d7-3586e4d663b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a73d57ac-ec3b-4f77-850b-89ce31785c1d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "95123b15-0280-4381-a8a3-1f950e1cfe18", new DateTime(2021, 6, 28, 15, 48, 5, 183, DateTimeKind.Utc).AddTicks(7233), "AQAAAAEAACcQAAAAEGG7VLj6FVKf30R8teZUhjESYdNJh+xTZThJvKjY26lr+fymq5/8WA3wp0aer5zz8g==", "ddea5c67-82d6-4321-a0e9-b10a6f0b23e1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "049ac095-be2a-4199-9971-dad51f3e17ec", new DateTime(2021, 6, 28, 15, 48, 5, 191, DateTimeKind.Utc).AddTicks(6309), "AQAAAAEAACcQAAAAEHZdlW1qN2DcFn1EXkFrGgYfR+PISdRSxHOohFsXzOPb6HR7+ylWKRLVQ4Iv2aTL7Q==", "0510603a-8edb-4fa0-9639-cee212d53d00" });

            migrationBuilder.UpdateData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Value" },
                values: new object[] { "Bachelor sonstiges", "Bachelor sonstiges" });

            migrationBuilder.InsertData(
                table: "Degree",
                columns: new[] { "Id", "Description", "Stage", "Value" },
                values: new object[] { 25, "Master of Arts (M.A.)", 3, "Master of Arts (M.A.)" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "68c7e8df-3173-4eb6-b641-4784c5d4a449");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d5c2eb60-6a75-4830-95e9-237803a3d2b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "469a4f7a-3de2-4067-a258-0b621ab58db4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e5bedf5-ca44-475b-b331-566271705418", new DateTime(2021, 6, 28, 15, 44, 47, 63, DateTimeKind.Utc).AddTicks(9158), "AQAAAAEAACcQAAAAEDcRHiq/zhbmzYYQL2l4DWgEPiJF3bBqcAXgWgYK01Br/PI+gMlbr3UYT2OMCMh1bg==", "1e7ecdd6-6820-45bc-9224-aa39b7f2a383" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d57f6ed-f0cf-48b2-851f-21440e6b4829", new DateTime(2021, 6, 28, 15, 44, 47, 83, DateTimeKind.Utc).AddTicks(4109), "AQAAAAEAACcQAAAAEMMdUrWtvMQwhE4RKfShZKTjvGjLJgRNDZWjS9CJifEakkRsx3PLN1t74ZAgew0yMA==", "016ad5d2-e497-413a-a1d9-0d457f606ab0" });

            migrationBuilder.UpdateData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Value" },
                values: new object[] { "Bachelor sonstiges•Master of Arts (M.A.)", "Bachelor sonstiges•Master of Arts (M.A.)" });
        }
    }
}
