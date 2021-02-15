using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00404_Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    owner_id = table.Column<Guid>(nullable: false),
                    const_obj_number = table.Column<string>(maxLength: 100, nullable: false),
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    region_id = table.Column<Guid>(nullable: false),
                    district_id = table.Column<Guid>(nullable: false),
                    code = table.Column<string>(maxLength: 20, nullable: false),
                    type_of_construction_object = table.Column<string>(maxLength: 50, nullable: false),
                    project_status = table.Column<string>(maxLength: 50, nullable: false),
                    type_of_financing = table.Column<string>(maxLength: 50, nullable: false),
                    cost = table.Column<decimal>(nullable: false),
                    implementation_state = table.Column<string>(maxLength: 50, nullable: false),
                    date_begin = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project", x => new { x.owner_id, x.const_obj_number });
                    table.ForeignKey(
                        name: "fk_project_atu_district_district_id",
                        column: x => x.district_id,
                        principalTable: "atu_district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_org_unit_owner_id",
                        column: x => x.owner_id,
                        principalTable: "org_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_atu_region_region_id",
                        column: x => x.region_id,
                        principalTable: "atu_region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_code",
                table: "project",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_project_district_id",
                table: "project",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_region_id",
                table: "project",
                column: "region_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project");
        }
    }
}
