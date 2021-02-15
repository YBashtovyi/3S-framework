using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00424_Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "repair_length",
                table: "project",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "repair_square",
                table: "project",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "type_of_project_work_id",
                table: "project",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "cdn_type_of_project_work",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    code = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    full_name = table.Column<string>(maxLength: 200, nullable: false),
                    parent_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cdn_type_of_project_work", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_type_of_project_work_id",
                table: "project",
                column: "type_of_project_work_id");

            migrationBuilder.AddForeignKey(
                name: "fk_project_cdn_type_of_project_work_type_of_project_work_id",
                table: "project",
                column: "type_of_project_work_id",
                principalTable: "cdn_type_of_project_work",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_project_cdn_type_of_project_work_type_of_project_work_id",
                table: "project");

            migrationBuilder.DropTable(
                name: "cdn_type_of_project_work");

            migrationBuilder.DropIndex(
                name: "ix_project_type_of_project_work_id",
                table: "project");

            migrationBuilder.DropColumn(
                name: "repair_length",
                table: "project");

            migrationBuilder.DropColumn(
                name: "repair_square",
                table: "project");

            migrationBuilder.DropColumn(
                name: "type_of_project_work_id",
                table: "project");
        }
    }
}
