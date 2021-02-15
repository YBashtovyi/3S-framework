using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00438_ProjectPhotoReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "prj_photo_report",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    project_id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    general_contractor_id = table.Column<Guid>(nullable: false),
                    doc_type = table.Column<string>(maxLength: 50, nullable: false),
                    reg_date = table.Column<DateTime>(nullable: false),
                    reg_number = table.Column<string>(maxLength: 100, nullable: false),
                    doc_state = table.Column<string>(maxLength: 50, nullable: false),
                    fixation_type = table.Column<string>(maxLength: 50, nullable: false),
                    fixation_state = table.Column<string>(maxLength: 50, nullable: false),
                    responsible_employee_id = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prj_photo_report", x => x.id);
                    table.ForeignKey(
                        name: "fk_prj_photo_report_organization_customer_id",
                        column: x => x.customer_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prj_photo_report_organization_general_contractor_id",
                        column: x => x.general_contractor_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prj_photo_report_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prj_photo_report_org_employee_responsible_employee_id",
                        column: x => x.responsible_employee_id,
                        principalTable: "org_employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_prj_photo_report_customer_id",
                table: "prj_photo_report",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_photo_report_general_contractor_id",
                table: "prj_photo_report",
                column: "general_contractor_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_photo_report_project_id",
                table: "prj_photo_report",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_photo_report_responsible_employee_id",
                table: "prj_photo_report",
                column: "responsible_employee_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prj_photo_report");
        }
    }
}
