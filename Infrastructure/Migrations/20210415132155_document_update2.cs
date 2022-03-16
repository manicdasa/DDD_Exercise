using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class document_update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8c443053-f8d5-4f60-84c1-c445230ca00c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3292c07d-b1eb-4b5c-ab2e-d9dbab6e44d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4d69c535-5e3e-4bb3-ba5a-be97bb1737d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c4c479a-3aa1-43b6-913d-e89a2b73c662", new DateTime(2021, 4, 15, 13, 21, 54, 121, DateTimeKind.Utc).AddTicks(9614), "AQAAAAEAACcQAAAAECB8cQptwBeARGE8Lvkywpcjsx/roFdTXxSMLr61YqUNH3drSpGVMCIVywLl036jww==", "33458791-335c-43d6-8853-df5e7c49c00e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3fb99fba-d1da-4eca-93e0-5fb2cd43bc92", new DateTime(2021, 4, 15, 13, 21, 54, 130, DateTimeKind.Utc).AddTicks(5699), "AQAAAAEAACcQAAAAEFySZjEngoKp7aXyI5Rzv7J+BpKIJsu/UFdki86pg+t0ZUp/uYPg7U/FgOKK55u96w==", "11a8fe63-e35e-4fbb-bfa6-1fcf7f510941" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "58dcefe5-e088-451f-adff-63149e412466");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "13e1da28-3913-4882-a887-7f37710a6404");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "43598694-9634-43c0-8286-13f49efcebc2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dacecbc0-751c-4e0b-a268-403bb9bc68a4", new DateTime(2021, 4, 15, 13, 21, 15, 97, DateTimeKind.Utc).AddTicks(5882), "AQAAAAEAACcQAAAAEN9E7LsDGe/hCkN5x+MVnfzUk6vhGZLMDiBCKPnJuIgUQKcAoBsPU8o/zn+P4dsxKA==", "c7a12e6d-f352-4cca-854a-acf7cbaa0f50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "beb86857-6eb4-423f-acc8-5f70c175b1e6", new DateTime(2021, 4, 15, 13, 21, 15, 104, DateTimeKind.Utc).AddTicks(9984), "AQAAAAEAACcQAAAAEMyVh2/SEl9Ju/1frdCxIESTPPzKPeDnDTKI7AfVEg+2lOL5lHLjmcWD+LqYK+uYQg==", "86192266-aafc-4817-8e58-892535eb9426" });
        }
    }
}
