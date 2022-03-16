using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class booking_document_mapping_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_BinaryDocument_BinaryDocumentId",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_BinaryDocument_BinaryDocumentId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestone_BinaryDocument_BinaryDocumentId",
                table: "Milestone");

            migrationBuilder.DropTable(
                name: "BinaryDocument");

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicName = table.Column<string>(type: "text", nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    LocalPath = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FileBinary = table.Column<byte>(type: "smallint", nullable: false),
                    BookingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "de276ba5-6791-4365-a481-e2390c7656db");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "36f1a284-afec-45d8-9111-58fd4f9dc8f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "779b1b33-dc96-4e8d-83ff-780bb217874d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90bc4415-ae36-4880-b931-669ea663dd68", "AQAAAAEAACcQAAAAEH6gr5pujaVYCMfz4Cm+TQON5bMK1iOIojxuu9lo8wGc3bONC86AhKYiQGQ54jv0Kw==", "b3c1442c-1be8-4ab1-8258-65d3c89aa678" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3b73c6e-3efc-457d-817f-5fe281b75288", "AQAAAAEAACcQAAAAEKRmM61EvPYocb1/DLiFQ30ei0bC576NH82CA5vX2rf2dHLMcoSEOYnG5DK7B+lp+g==", "b38f3dd5-df70-41b8-a349-36ec0a5d1305" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 24, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Document_BookingId",
                table: "Document",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_Document_BinaryDocumentId",
                table: "Conversation",
                column: "BinaryDocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Document_BinaryDocumentId",
                table: "Message",
                column: "BinaryDocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Milestone_Document_BinaryDocumentId",
                table: "Milestone",
                column: "BinaryDocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_Document_BinaryDocumentId",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Document_BinaryDocumentId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestone_Document_BinaryDocumentId",
                table: "Milestone");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.CreateTable(
                name: "BinaryDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileBinary = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinaryDocument", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ecbce56d-3ff6-4704-9ef1-8819fb36f416");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "22e37ee6-d7e0-4eb9-bf44-81ac2b8ed282");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7c90779f-0e5c-4439-8d60-561844b95b54");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac0024d6-e54e-4172-b900-1de1c58e7b8b", "AQAAAAEAACcQAAAAEF2II67eWvENfEFtVRMJl+759jymCtFbJg0M9RydYCfG/ei9W3dUAAzLpxfvVg9g1Q==", "686e312b-1191-4ba8-a05c-7aba6d824235" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b25c72fd-9a5d-4407-a7df-0f6b8298c5ea", "AQAAAAEAACcQAAAAEN5p01SD+BtA6MbhA7nWEACX9EdyvV2esEteZfJt5OJ+s1bLMCIhkjSHcVUzpFoV4g==", "7b21ce36-709d-4ec4-8136-d4b861f3e636" });

            migrationBuilder.UpdateData(
                table: "ServiceCharge",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2021, 3, 23, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_BinaryDocument_BinaryDocumentId",
                table: "Conversation",
                column: "BinaryDocumentId",
                principalTable: "BinaryDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_BinaryDocument_BinaryDocumentId",
                table: "Message",
                column: "BinaryDocumentId",
                principalTable: "BinaryDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Milestone_BinaryDocument_BinaryDocumentId",
                table: "Milestone",
                column: "BinaryDocumentId",
                principalTable: "BinaryDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
