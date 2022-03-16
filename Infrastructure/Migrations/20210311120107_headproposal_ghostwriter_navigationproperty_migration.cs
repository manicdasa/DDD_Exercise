using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class headproposal_ghostwriter_navigationproperty_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GHWId",
                table: "HeadProposal",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "72de7824-1cea-43cc-b18f-5b2d0992e828");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8a668bb5-3a08-4694-ab43-e2e2f67fb3a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4b9976f9-61cb-4984-a7e7-a725fc4eb459");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e0ed873-9b07-49ba-8198-c8efe001c328", "AQAAAAEAACcQAAAAEHxNcFsLZDULEM3bbhMzBM0TTvAy+vUkiFC2yD9s/suoJawt2P/Wg1ukpkT6oc2MfA==", "8c011d2c-9bb0-48ae-b1b1-607f45d80fb5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bed30f35-73ac-46a8-9c3a-64d979eb04b5", "AQAAAAEAACcQAAAAEBNQcsXCvRldsED2gUAPAh7MucJfC+PCqotEu+R8JhZV81D9fg7NQMwO1WzIyhLRhQ==", "8bde0212-e815-45a0-9c0a-cbf0f4b1ba6b" });

            migrationBuilder.CreateIndex(
                name: "IX_HeadProposal_GHWId",
                table: "HeadProposal",
                column: "GHWId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadProposal_AspNetUsers_GHWId",
                table: "HeadProposal",
                column: "GHWId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadProposal_AspNetUsers_GHWId",
                table: "HeadProposal");

            migrationBuilder.DropIndex(
                name: "IX_HeadProposal_GHWId",
                table: "HeadProposal");

            migrationBuilder.DropColumn(
                name: "GHWId",
                table: "HeadProposal");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6d24e978-4870-4a4c-a510-8ed551468f07");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "85214152-9bb5-4581-b708-0c9c4f2d6469");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "01772618-a710-46a0-843c-74d00612af23");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87e271b2-24a9-4ee9-99fd-4a16dc52b820", "AQAAAAEAACcQAAAAENu7bLgsXrIcMNrYW7oJnYEloP9KUzOH+ohaBYJoDAhkt4Vsxb+WoJ2qkOgRDTPDFQ==", "283a1540-c6d9-41fe-a6bb-4fbde7095520" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe200599-990f-409f-804d-51560c6e5914", "AQAAAAEAACcQAAAAEMMmoxQpL9AxRzqRxFO8AYjOdYpASwqT2rMeOGuYu2zK/l5kVrBGPkkc7etbkA7kUQ==", "89219580-4426-4747-9e9c-8ff895fb5585" });
        }
    }
}
