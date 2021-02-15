using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00438_Add_CityRegionMap_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "atu_city_region_map",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    city_id = table.Column<Guid>(nullable: false),
                    region_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atu_city_region_map", x => x.id);
                    table.ForeignKey(
                        name: "fk_atu_city_region_map_atu_city_city_id",
                        column: x => x.city_id,
                        principalTable: "atu_city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_atu_city_region_map_atu_region_region_id",
                        column: x => x.region_id,
                        principalTable: "atu_region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_atu_city_region_map_city_id",
                table: "atu_city_region_map",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_atu_city_region_map_region_id",
                table: "atu_city_region_map",
                column: "region_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atu_city_region_map");
        }
    }
}
