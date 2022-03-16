using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GhostWriter.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Buzzword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buzzword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degree", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpertiseArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertiseArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KindOfWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KindOfWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    SeoFilename = table.Column<string>(type: "text", nullable: true),
                    VirtualPath = table.Column<string>(type: "text", nullable: true),
                    BinaryData = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceChargeType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceChargeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PlannedBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    CalculatedServiceCharges = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectTopic = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PagesNo = table.Column<int>(type: "integer", nullable: false),
                    ProjectStatus = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    MinimumDegreeId = table.Column<int>(type: "integer", nullable: true),
                    LanguageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_Degree_MinimumDegreeId",
                        column: x => x.MinimumDegreeId,
                        principalTable: "Degree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalSpent = table.Column<decimal>(type: "numeric", nullable: false),
                    JobsPostedCnt = table.Column<int>(type: "integer", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: true),
                    PricePerPage = table.Column<decimal>(type: "numeric", nullable: false),
                    AvgPricePerPage = table.Column<decimal>(type: "numeric", nullable: true),
                    DirectBooking = table.Column<bool>(type: "boolean", nullable: false),
                    PagesPerDay = table.Column<int>(type: "integer", nullable: false),
                    ProfileIntroduction = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PictureId = table.Column<int>(type: "integer", nullable: true),
                    HighestDegreeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleData_Degree_HighestDegreeId",
                        column: x => x.HighestDegreeId,
                        principalTable: "Degree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoleData_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCharge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPercentage = table.Column<bool>(type: "boolean", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ServiceChargeTypeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCharge_ServiceChargeType_ServiceChargeTypeId",
                        column: x => x.ServiceChargeTypeId,
                        principalTable: "ServiceChargeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuzzwordProject",
                columns: table => new
                {
                    BuzzwordsId = table.Column<int>(type: "integer", nullable: false),
                    ProjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuzzwordProject", x => new { x.BuzzwordsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_BuzzwordProject_Buzzword_BuzzwordsId",
                        column: x => x.BuzzwordsId,
                        principalTable: "Buzzword",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuzzwordProject_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertiseAreaProject",
                columns: table => new
                {
                    ExpertiseAreasId = table.Column<int>(type: "integer", nullable: false),
                    ProjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertiseAreaProject", x => new { x.ExpertiseAreasId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_ExpertiseAreaProject_ExpertiseArea_ExpertiseAreasId",
                        column: x => x.ExpertiseAreasId,
                        principalTable: "ExpertiseArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertiseAreaProject_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeadProposal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    ProposalType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadProposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeadProposal_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserRoleDataId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_UserRoleData_UserRoleDataId",
                        column: x => x.UserRoleDataId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuzzwordUserRoleData",
                columns: table => new
                {
                    BuzzwordsId = table.Column<int>(type: "integer", nullable: false),
                    UserRoleDatasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuzzwordUserRoleData", x => new { x.BuzzwordsId, x.UserRoleDatasId });
                    table.ForeignKey(
                        name: "FK_BuzzwordUserRoleData_Buzzword_BuzzwordsId",
                        column: x => x.BuzzwordsId,
                        principalTable: "Buzzword",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuzzwordUserRoleData_UserRoleData_UserRoleDatasId",
                        column: x => x.UserRoleDatasId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertiseAreaUserRoleData",
                columns: table => new
                {
                    ExpertiseAreasId = table.Column<int>(type: "integer", nullable: false),
                    UserRoleDatasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertiseAreaUserRoleData", x => new { x.ExpertiseAreasId, x.UserRoleDatasId });
                    table.ForeignKey(
                        name: "FK_ExpertiseAreaUserRoleData_ExpertiseArea_ExpertiseAreasId",
                        column: x => x.ExpertiseAreasId,
                        principalTable: "ExpertiseArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertiseAreaUserRoleData_UserRoleData_UserRoleDatasId",
                        column: x => x.UserRoleDatasId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KindOfWorkUserRoleData",
                columns: table => new
                {
                    KindOfWorksId = table.Column<int>(type: "integer", nullable: false),
                    UserRoleDatasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KindOfWorkUserRoleData", x => new { x.KindOfWorksId, x.UserRoleDatasId });
                    table.ForeignKey(
                        name: "FK_KindOfWorkUserRoleData_KindOfWork_KindOfWorksId",
                        column: x => x.KindOfWorksId,
                        principalTable: "KindOfWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KindOfWorkUserRoleData_UserRoleData_UserRoleDatasId",
                        column: x => x.UserRoleDatasId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageUserRoleData",
                columns: table => new
                {
                    LanguagesId = table.Column<int>(type: "integer", nullable: false),
                    UserRoleDatasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageUserRoleData", x => new { x.LanguagesId, x.UserRoleDatasId });
                    table.ForeignKey(
                        name: "FK_LanguageUserRoleData_Language_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageUserRoleData_UserRoleData_UserRoleDatasId",
                        column: x => x.UserRoleDatasId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectServiceCharge",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "integer", nullable: false),
                    ServiceChargesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectServiceCharge", x => new { x.ProjectsId, x.ServiceChargesId });
                    table.ForeignKey(
                        name: "FK_ProjectServiceCharge_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectServiceCharge_ServiceCharge_ServiceChargesId",
                        column: x => x.ServiceChargesId,
                        principalTable: "ServiceCharge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlagueScanned = table.Column<bool>(type: "boolean", nullable: false),
                    GHWReceivedConfirmation = table.Column<bool>(type: "boolean", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalServiceCharges = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    HeadProposalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_HeadProposal_HeadProposalId",
                        column: x => x.HeadProposalId,
                        principalTable: "HeadProposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HeadProposalId = table.Column<int>(type: "integer", nullable: false),
                    BinaryDocumentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversation_BinaryDocument_BinaryDocumentId",
                        column: x => x.BinaryDocumentId,
                        principalTable: "BinaryDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversation_HeadProposal_HeadProposalId",
                        column: x => x.HeadProposalId,
                        principalTable: "HeadProposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proposal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FinancialOffer = table.Column<float>(type: "real", nullable: false),
                    FinancialOfferWithCharges = table.Column<float>(type: "real", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MilestonesCnt = table.Column<int>(type: "integer", nullable: false),
                    PersonalMessage = table.Column<string>(type: "text", nullable: true),
                    ProposalType = table.Column<int>(type: "integer", nullable: false),
                    HeadProposalId = table.Column<int>(type: "integer", nullable: true),
                    ParentProposalId = table.Column<int>(type: "integer", nullable: false),
                    ChildProposalId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposal_HeadProposal_HeadProposalId",
                        column: x => x.HeadProposalId,
                        principalTable: "HeadProposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposal_Proposal_ChildProposalId",
                        column: x => x.ChildProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dispute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Resolution = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BookingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispute_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Milestone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DatePlanned = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateRealised = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsFinalMilestone = table.Column<bool>(type: "boolean", nullable: false),
                    MilestoneStatus = table.Column<int>(type: "integer", nullable: false),
                    BookingId = table.Column<int>(type: "integer", nullable: true),
                    BinaryDocumentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milestone_BinaryDocument_BinaryDocumentId",
                        column: x => x.BinaryDocumentId,
                        principalTable: "BinaryDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Milestone_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentCharges = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    BookingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StarRating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    RateWriter = table.Column<int>(type: "integer", nullable: false),
                    BookingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rate_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageText = table.Column<string>(type: "text", nullable: true),
                    DateTimeSent = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ConversationId = table.Column<int>(type: "integer", nullable: true),
                    BinaryDocumentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_BinaryDocument_BinaryDocumentId",
                        column: x => x.BinaryDocumentId,
                        principalTable: "BinaryDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProposalStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProposalStatus = table.Column<int>(type: "integer", nullable: false),
                    ApprovedByCustomer = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovedByGhw = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProposalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalStatusHistory_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "e4efcc24-9714-46d1-b71f-d6478e6c5e9b", "Admin", "ADMIN" },
                    { 2, "966e3b56-0615-4a25-bc87-638bbb5fe0f4", "Customer", "CUSTOMER" },
                    { 3, "14553bbb-9769-47ee-89e5-c06870228e12", "Ghostwriter", "GHOSTWRITER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "987bfae7-f3d2-4430-9c4b-61e2ff6459b7", "aleksandar_stojcic@code3profit.com", false, false, null, "ALEKSANDAR_STOJCIC@CODE3PROFIT.COM", "ALEKSANDAR_STOJCIC@CODE3PROFIT.COM", "AQAAAAEAACcQAAAAECkyj96MsoG+J9EjTHzOPXV6Cn2eKfjk0CQBvTtBtpDsKy9xUsvOMOZTkWtUBmncAA==", null, false, "80730624-5495-4705-9547-898505c22baa", false, "aleksandar_stojcic@code3profit.com" },
                    { 2, 0, "f98708f2-de0d-4596-96a0-25e7bc02d928", "dasa_manic@code3profit.com", false, false, null, "DASA_MANIC@CODE3PROFIT.COM", "DASA_MANIC@CODE3PROFIT.COM", "AQAAAAEAACcQAAAAENhRyUOSSUMvD9Pn1UeshB+QGOCaELFcXSNc2Z1gwqLUtMI+xZ4UsR6QYtsd85E34g==", null, false, "5fa8d778-abc9-4e71-a06a-70ffaca0ad38", false, "dasa_manic@code3profit.com" }
                });

            migrationBuilder.InsertData(
                table: "ServiceCharge",
                columns: new[] { "Id", "ChargeAmount", "EndDate", "IsDefault", "IsPercentage", "ServiceChargeTypeId", "StartDate" },
                values: new object[,]
                {
                    { 1, 20m, null, true, false, null, new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 20m, new DateTime(2021, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ServiceChargeType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Default Service Charge", "Default Service Charge" },
                    { 2, "Service Charge", "Service Charge" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "UserRoleDataId" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 1, 2, null },
                    { 2, 2, null },
                    { 3, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserRoleDataId",
                table: "AspNetUserRoles",
                column: "UserRoleDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_HeadProposalId",
                table: "Booking",
                column: "HeadProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_BuzzwordProject_ProjectsId",
                table: "BuzzwordProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_BuzzwordUserRoleData_UserRoleDatasId",
                table: "BuzzwordUserRoleData",
                column: "UserRoleDatasId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_BinaryDocumentId",
                table: "Conversation",
                column: "BinaryDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_HeadProposalId",
                table: "Conversation",
                column: "HeadProposalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dispute_BookingId",
                table: "Dispute",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertiseAreaProject_ProjectsId",
                table: "ExpertiseAreaProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertiseAreaUserRoleData_UserRoleDatasId",
                table: "ExpertiseAreaUserRoleData",
                column: "UserRoleDatasId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadProposal_ProjectId",
                table: "HeadProposal",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_KindOfWorkProject_ProjectsId",
                table: "KindOfWorkProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_KindOfWorkUserRoleData_UserRoleDatasId",
                table: "KindOfWorkUserRoleData",
                column: "UserRoleDatasId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageUserRoleData_UserRoleDatasId",
                table: "LanguageUserRoleData",
                column: "UserRoleDatasId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_BinaryDocumentId",
                table: "Message",
                column: "BinaryDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationId",
                table: "Message",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestone_BinaryDocumentId",
                table: "Milestone",
                column: "BinaryDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestone_BookingId",
                table: "Milestone",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BookingId",
                table: "Payment",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerId",
                table: "Project",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LanguageId",
                table: "Project",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_MinimumDegreeId",
                table: "Project",
                column: "MinimumDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectServiceCharge_ServiceChargesId",
                table: "ProjectServiceCharge",
                column: "ServiceChargesId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_ChildProposalId",
                table: "Proposal",
                column: "ChildProposalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_HeadProposalId",
                table: "Proposal",
                column: "HeadProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalStatusHistory_ProposalId",
                table: "ProposalStatusHistory",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_BookingId",
                table: "Rate",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharge_ServiceChargeTypeId",
                table: "ServiceCharge",
                column: "ServiceChargeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleData_HighestDegreeId",
                table: "UserRoleData",
                column: "HighestDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleData_PictureId",
                table: "UserRoleData",
                column: "PictureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BuzzwordProject");

            migrationBuilder.DropTable(
                name: "BuzzwordUserRoleData");

            migrationBuilder.DropTable(
                name: "Dispute");

            migrationBuilder.DropTable(
                name: "ExpertiseAreaProject");

            migrationBuilder.DropTable(
                name: "ExpertiseAreaUserRoleData");

            migrationBuilder.DropTable(
                name: "KindOfWorkProject");

            migrationBuilder.DropTable(
                name: "KindOfWorkUserRoleData");

            migrationBuilder.DropTable(
                name: "LanguageUserRoleData");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Milestone");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "ProjectServiceCharge");

            migrationBuilder.DropTable(
                name: "ProposalStatusHistory");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Buzzword");

            migrationBuilder.DropTable(
                name: "ExpertiseArea");

            migrationBuilder.DropTable(
                name: "KindOfWork");

            migrationBuilder.DropTable(
                name: "UserRoleData");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "ServiceCharge");

            migrationBuilder.DropTable(
                name: "Proposal");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "BinaryDocument");

            migrationBuilder.DropTable(
                name: "ServiceChargeType");

            migrationBuilder.DropTable(
                name: "HeadProposal");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
