using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class proposal_properties_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_Proposal_ChildProposalId",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "ApprovedByCustomer",
                table: "ProposalStatusHistory");

            migrationBuilder.DropColumn(
                name: "ApprovedByGhw",
                table: "ProposalStatusHistory");

            migrationBuilder.AlterColumn<int>(
                name: "ParentProposalId",
                table: "Proposal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinancialOfferWithCharges",
                table: "Proposal",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinancialOffer",
                table: "Proposal",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "ChildProposalId",
                table: "Proposal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b02d1926-02cf-4904-96f6-e03894f11cb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "67b9375a-409f-44ef-bebc-ab49a6394e6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ef006aae-2aa2-47d1-9cac-4cf8d2f9bc4b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "945db8e3-7e47-4a24-8ede-4375979da260", "AQAAAAEAACcQAAAAELTMoij129X0i+URf8AOTSzeIMqErUFv75mzlwRMDU6IWzcEhR4APMWqAHm1Q5l7sw==", "c17a9449-6773-4a42-9db5-6dd31c6b6b4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d33da33-e502-4e46-972b-bbfd312ff1de", "AQAAAAEAACcQAAAAEOUDf5L34dNNsmDGw4FLSPJcoIt4VnQBpX+Fwq3aIA59WDfh7LsGYkAdZNTUM3lfAQ==", "dcb1bc3e-7500-4b58-801f-f2de4bbe5e12" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_Proposal_ChildProposalId",
                table: "Proposal",
                column: "ChildProposalId",
                principalTable: "Proposal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_Proposal_ChildProposalId",
                table: "Proposal");

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedByCustomer",
                table: "ProposalStatusHistory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedByGhw",
                table: "ProposalStatusHistory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ParentProposalId",
                table: "Proposal",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "FinancialOfferWithCharges",
                table: "Proposal",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "FinancialOffer",
                table: "Proposal",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "ChildProposalId",
                table: "Proposal",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fbde7ea0-a279-491c-b6a8-eb894719c154");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "259f6528-3c3e-46b7-82b9-0f9e99456510");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "59aee982-3fbf-49ea-89be-d1ac268f25a2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d813b25b-e4c9-4bfa-805a-a394cb0dc3b6", "AQAAAAEAACcQAAAAEFOg83WKVwmHbu1PV7bXccb/nHO6jau6cXauSMcA029jViTpyUPMMTK5zap+B4TveQ==", "d677f907-d53c-460d-ba2c-81d7c782aa42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8cdb1c3b-ad09-4e7b-98a5-d0f308e47fdc", "AQAAAAEAACcQAAAAEF4sKRouVwwc7x1JcdC9IDUAsw91aFI9NM08mdBkQ+WWcUVtsoPrpV6+xfz/iqoJbQ==", "bf48457d-a3e8-427d-ab55-a58d72dc8f16" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_Proposal_ChildProposalId",
                table: "Proposal",
                column: "ChildProposalId",
                principalTable: "Proposal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
