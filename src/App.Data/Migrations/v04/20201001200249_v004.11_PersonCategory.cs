using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00411_PersonCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cmn_person_population_category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cmn_person_population_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    population_category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    record_state = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmn_person_population_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_cmn_person_population_category_cmn_person_person_id",
                        column: x => x.person_id,
                        principalTable: "cmn_person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cmn_person_population_category_cmn_enum_record_population_c~",
                        column: x => x.population_category_id,
                        principalTable: "cmn_enum_record",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cmn_person_population_category_person_id",
                table: "cmn_person_population_category",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_cmn_person_population_category_population_category_id",
                table: "cmn_person_population_category",
                column: "population_category_id");
        }
    }
}
