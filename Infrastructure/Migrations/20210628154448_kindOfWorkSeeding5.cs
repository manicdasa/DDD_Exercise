using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class kindOfWorkSeeding5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "ExpertiseArea",
                columns: new[] { "Id", "Description", "FieldStatus", "Value" },
                values: new object[,]
                {
                    { 122, "Essay", 1, "Essay" },
                    { 123, "Case Study", 1, "Case Study" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "ExpertiseArea",
                keyColumn: "Id",
                keyValue: 123);

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
        }
    }
}
