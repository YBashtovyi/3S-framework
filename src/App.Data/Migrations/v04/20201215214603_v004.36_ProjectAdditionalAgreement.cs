using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00436_ProjectAdditionalAgreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prj_work_schedule_sub_type_cdn_work_sub_type_work_sub_type_~",
                table: "prj_work_schedule_sub_type");

            migrationBuilder.CreateTable(
                name: "prj_additional_agreement",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    project_id = table.Column<Guid>(nullable: false),
                    parent_id = table.Column<Guid>(nullable: false),
                    doc_type = table.Column<string>(maxLength: 50, nullable: false),
                    reg_date = table.Column<DateTime>(nullable: false),
                    reg_number = table.Column<string>(maxLength: 100, nullable: false),
                    doc_state = table.Column<string>(maxLength: 50, nullable: false),
                    cost = table.Column<decimal>(nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prj_additional_agreement", x => x.id);
                    table.ForeignKey(
                        name: "fk_prj_additional_agreement_prj_contract_parent_id",
                        column: x => x.parent_id,
                        principalTable: "prj_contract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prj_additional_agreement_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_prj_additional_agreement_parent_id",
                table: "prj_additional_agreement",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_additional_agreement_project_id",
                table: "prj_additional_agreement",
                column: "project_id");

            migrationBuilder.AddForeignKey(
                name: "fk_prj_work_schedule_sub_type_cdn_work_sub_type_work_sub_type_id",
                table: "prj_work_schedule_sub_type",
                column: "work_sub_type_id",
                principalTable: "cdn_work_sub_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prj_work_schedule_sub_type_cdn_work_sub_type_work_sub_type_id",
                table: "prj_work_schedule_sub_type");

            migrationBuilder.DropTable(
                name: "prj_additional_agreement");

            migrationBuilder.AddForeignKey(
                name: "fk_prj_work_schedule_sub_type_cdn_work_sub_type_work_sub_type_~",
                table: "prj_work_schedule_sub_type",
                column: "work_sub_type_id",
                principalTable: "cdn_work_sub_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
