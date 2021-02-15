using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00430_ProjectWorkSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cdn_work_sub_type",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    code = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    measurement_unit = table.Column<string>(maxLength: 50, nullable: false),
                    classifier_type = table.Column<string>(maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cdn_work_sub_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prj_work_schedule",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    project_id = table.Column<Guid>(nullable: false),
                    doc_type = table.Column<string>(maxLength: 50, nullable: false),
                    parent_id = table.Column<Guid>(nullable: true),
                    reg_date = table.Column<DateTime>(nullable: false),
                    reg_number = table.Column<string>(maxLength: 100, nullable: false),
                    doc_state = table.Column<string>(maxLength: 50, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prj_work_schedule", x => x.id);
                    table.ForeignKey(
                        name: "fk_prj_work_schedule_cmn_document_parent_id",
                        column: x => x.parent_id,
                        principalTable: "cmn_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_prj_work_schedule_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prj_work_schedule_stage",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    prj_work_schedule_id = table.Column<Guid>(nullable: false),
                    stage_number = table.Column<string>(maxLength: 50, nullable: false),
                    begin_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false),
                    cost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prj_work_schedule_stage", x => x.id);
                    table.ForeignKey(
                        name: "fk_prj_work_schedule_stage_cmn_document_prj_work_schedule_id",
                        column: x => x.prj_work_schedule_id,
                        principalTable: "cmn_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prj_work_schedule_sub_type",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    prj_work_schedule_id = table.Column<Guid>(nullable: false),
                    prj_work_schedule_stage_id = table.Column<Guid>(nullable: false),
                    work_sub_type_id = table.Column<Guid>(nullable: false),
                    measurement_unit = table.Column<string>(maxLength: 50, nullable: false),
                    amount = table.Column<float>(nullable: false),
                    target = table.Column<float>(nullable: false),
                    begin_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false),
                    prj_project_work_schedule_stage_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prj_work_schedule_sub_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_prj_work_schedule_sub_type_prj_work_schedule_stage_prj_proj~",
                        column: x => x.prj_project_work_schedule_stage_id,
                        principalTable: "prj_work_schedule_stage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_prj_work_schedule_sub_type_cmn_document_prj_work_schedule_id",
                        column: x => x.prj_work_schedule_id,
                        principalTable: "cmn_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prj_work_schedule_sub_type_cdn_work_sub_type_work_sub_type_~",
                        column: x => x.work_sub_type_id,
                        principalTable: "cdn_work_sub_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_prj_work_schedule_parent_id",
                table: "prj_work_schedule",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_work_schedule_project_id",
                table: "prj_work_schedule",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_work_schedule_stage_prj_work_schedule_id",
                table: "prj_work_schedule_stage",
                column: "prj_work_schedule_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_work_schedule_sub_type_prj_project_work_schedule_stage_~",
                table: "prj_work_schedule_sub_type",
                column: "prj_project_work_schedule_stage_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_work_schedule_sub_type_prj_work_schedule_id",
                table: "prj_work_schedule_sub_type",
                column: "prj_work_schedule_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_work_schedule_sub_type_work_sub_type_id",
                table: "prj_work_schedule_sub_type",
                column: "work_sub_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prj_work_schedule");

            migrationBuilder.DropTable(
                name: "prj_work_schedule_sub_type");

            migrationBuilder.DropTable(
                name: "prj_work_schedule_stage");

            migrationBuilder.DropTable(
                name: "cdn_work_sub_type");
        }
    }
}
