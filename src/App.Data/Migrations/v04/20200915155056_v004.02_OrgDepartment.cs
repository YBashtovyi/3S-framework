using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00402_OrgDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_org_department_cmn_enum_record_department_type_id",
                table: "org_department");

            migrationBuilder.DropIndex(
                name: "ix_org_department_department_type_id",
                table: "org_department");

            migrationBuilder.DropColumn(
                name: "department_type_id",
                table: "org_department");

            migrationBuilder.AddColumn<string>(
                name: "department_type",
                table: "org_department",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department_type",
                table: "org_department");

            migrationBuilder.AddColumn<Guid>(
                name: "department_type_id",
                table: "org_department",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_org_department_department_type_id",
                table: "org_department",
                column: "department_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_org_department_cmn_enum_record_department_type_id",
                table: "org_department",
                column: "department_type_id",
                principalTable: "cmn_enum_record",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
