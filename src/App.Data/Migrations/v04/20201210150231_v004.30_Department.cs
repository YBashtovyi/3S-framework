using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00430_Department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_org_department_organization_organization_id",
                table: "org_department");

            migrationBuilder.DropIndex(
                name: "ix_org_department_organization_id",
                table: "org_department");

            migrationBuilder.DropColumn(
                name: "category",
                table: "org_department");

            migrationBuilder.DropColumn(
                name: "location",
                table: "org_department");

            migrationBuilder.DropColumn(
                name: "organization_id",
                table: "org_department");

            migrationBuilder.DropColumn(
                name: "zip_code",
                table: "org_department");

            migrationBuilder.AddColumn<string>(
                name: "caption",
                table: "org_department",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "org_department",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_org_department_parent_id",
                table: "org_department",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "fk_org_department_org_unit_parent_id",
                table: "org_department",
                column: "parent_id",
                principalTable: "org_unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_org_department_org_unit_parent_id",
                table: "org_department");

            migrationBuilder.DropIndex(
                name: "ix_org_department_parent_id",
                table: "org_department");

            migrationBuilder.DropColumn(
                name: "caption",
                table: "org_department");

            migrationBuilder.DropColumn(
                name: "name",
                table: "org_department");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "org_department",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "org_department",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "organization_id",
                table: "org_department",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "zip_code",
                table: "org_department",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_org_department_organization_id",
                table: "org_department",
                column: "organization_id");

            migrationBuilder.AddForeignKey(
                name: "fk_org_department_organization_organization_id",
                table: "org_department",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
