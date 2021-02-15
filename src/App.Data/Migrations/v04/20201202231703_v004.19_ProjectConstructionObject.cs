using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00419_ProjectConstructionObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type_of_construction_object",
                table: "project");

            migrationBuilder.CreateTable(
                name: "prj_construction_object",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    project_id = table.Column<Guid>(nullable: false),
                    construction_object_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prj_construction_object", x => x.id);
                    table.ForeignKey(
                        name: "fk_prj_construction_object_construction_object_construction_ob~",
                        column: x => x.construction_object_id,
                        principalTable: "construction_object",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prj_construction_object_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_prj_construction_object_construction_object_id",
                table: "prj_construction_object",
                column: "construction_object_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_construction_object_project_id",
                table: "prj_construction_object",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prj_construction_object");

            migrationBuilder.AddColumn<string>(
                name: "type_of_construction_object",
                table: "project",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
