using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Focus.Incident.Infrastructure.DB.Migrations.development.ApplicationDb
{
    public partial class ApplicationDb_initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationName = table.Column<string>(nullable: true),
                    Counter = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGroup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGroupCount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignmentGroup = table.Column<string>(nullable: true),
                    Counter = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentGroupCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessLineCount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusinessLine = table.Column<string>(nullable: true),
                    Counter = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLineCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonCount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    personName = table.Column<string>(nullable: true),
                    Counter = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryBusinessLine",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryBusinessLine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SummaryCount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestCounter = table.Column<int>(nullable: false),
                    IncidentCounter = table.Column<int>(nullable: false),
                    SOICounter = table.Column<int>(nullable: false),
                    NetcoolCounter = table.Column<int>(nullable: false),
                    TotalIncidents = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummaryCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incident",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Resolved = table.Column<DateTime>(nullable: true),
                    Closed = table.Column<DateTime>(nullable: true),
                    Last_Updated = table.Column<DateTime>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    CMDB_CI_Company = table.Column<string>(nullable: true),
                    Country_Impacted = table.Column<string>(nullable: true),
                    PrimaryBusinessLineId = table.Column<long>(nullable: true),
                    ApplicationId = table.Column<long>(nullable: true),
                    AssignmentGroupId = table.Column<long>(nullable: true),
                    Assigned_toId = table.Column<long>(nullable: true),
                    Caused_By = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Business_Impact = table.Column<float>(nullable: true),
                    Caller_ID = table.Column<string>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Resolution_Description = table.Column<string>(nullable: true),
                    Vendor = table.Column<string>(nullable: true),
                    Major_Incident = table.Column<bool>(nullable: false),
                    Problem_ID = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Business_Area = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incident_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incident_Person_Assigned_toId",
                        column: x => x.Assigned_toId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incident_AssignmentGroup_AssignmentGroupId",
                        column: x => x.AssignmentGroupId,
                        principalTable: "AssignmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incident_PrimaryBusinessLine_PrimaryBusinessLineId",
                        column: x => x.PrimaryBusinessLineId,
                        principalTable: "PrimaryBusinessLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incident_ApplicationId",
                table: "Incident",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_Assigned_toId",
                table: "Incident",
                column: "Assigned_toId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_AssignmentGroupId",
                table: "Incident",
                column: "AssignmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_PrimaryBusinessLineId",
                table: "Incident",
                column: "PrimaryBusinessLineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationCount");

            migrationBuilder.DropTable(
                name: "AssignmentGroupCount");

            migrationBuilder.DropTable(
                name: "BusinessLineCount");

            migrationBuilder.DropTable(
                name: "Incident");

            migrationBuilder.DropTable(
                name: "PersonCount");

            migrationBuilder.DropTable(
                name: "SummaryCount");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "AssignmentGroup");

            migrationBuilder.DropTable(
                name: "PrimaryBusinessLine");
        }
    }
}
