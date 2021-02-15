using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00433_ProjectWorkScheduleSubType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prj_work_schedule_sub_type_prj_work_schedule_stage_prj_proj~",
                table: "prj_work_schedule_sub_type");

            migrationBuilder.DropIndex(
                name: "ix_prj_work_schedule_sub_type_prj_project_work_schedule_stage_~",
                table: "prj_work_schedule_sub_type");

            migrationBuilder.DropColumn(
                name: "prj_project_work_schedule_stage_id",
                table: "prj_work_schedule_sub_type");

            migrationBuilder.CreateIndex(
                name: "ix_prj_work_schedule_sub_type_prj_work_schedule_stage_id",
                table: "prj_work_schedule_sub_type",
                column: "prj_work_schedule_stage_id");

            migrationBuilder.AddForeignKey(
                name: "fk_prj_work_schedule_sub_type_prj_work_schedule_stage_prj_work~",
                table: "prj_work_schedule_sub_type",
                column: "prj_work_schedule_stage_id",
                principalTable: "prj_work_schedule_stage",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prj_work_schedule_sub_type_prj_work_schedule_stage_prj_work~",
                table: "prj_work_schedule_sub_type");

            migrationBuilder.DropIndex(
                name: "ix_prj_work_schedule_sub_type_prj_work_schedule_stage_id",
                table: "prj_work_schedule_sub_type");

            migrationBuilder.AddColumn<Guid>(
                name: "prj_project_work_schedule_stage_id",
                table: "prj_work_schedule_sub_type",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_prj_work_schedule_sub_type_prj_project_work_schedule_stage_~",
                table: "prj_work_schedule_sub_type",
                column: "prj_project_work_schedule_stage_id");

            migrationBuilder.AddForeignKey(
                name: "fk_prj_work_schedule_sub_type_prj_work_schedule_stage_prj_proj~",
                table: "prj_work_schedule_sub_type",
                column: "prj_project_work_schedule_stage_id",
                principalTable: "prj_work_schedule_stage",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
