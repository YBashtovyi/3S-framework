using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00401_OrgEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_org_employee_cmn_enum_record_position_type_id",
                table: "org_employee");

            migrationBuilder.DropIndex(
                name: "ix_org_employee_position_type_id",
                table: "org_employee");

            migrationBuilder.DropColumn(
                name: "department_section_id",
                table: "org_employee");

            migrationBuilder.DropColumn(
                name: "position_type_id",
                table: "org_employee");

            migrationBuilder.DropColumn(
                name: "working_start_date",
                table: "org_employee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "department_section_id",
                table: "org_employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "position_type_id",
                table: "org_employee",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "working_start_date",
                table: "org_employee",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_org_employee_position_type_id",
                table: "org_employee",
                column: "position_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_org_employee_cmn_enum_record_position_type_id",
                table: "org_employee",
                column: "position_type_id",
                principalTable: "cmn_enum_record",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
