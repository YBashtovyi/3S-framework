using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00421_ConstructionObjectExtendedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "construction_object_extended_property",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    construction_object_id = table.Column<Guid>(nullable: false),
                    dictionary_id = table.Column<Guid>(nullable: false),
                    value = table.Column<string>(nullable: false),
                    value_json = table.Column<string>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_construction_object_extended_property", x => x.id);
                    table.ForeignKey(
                        name: "fk_construction_object_extended_property_construction_object_c~",
                        column: x => x.construction_object_id,
                        principalTable: "construction_object",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_construction_object_extended_property_construction_object_e~",
                        column: x => x.dictionary_id,
                        principalTable: "construction_object_ex_property_dictionary",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_construction_object_extended_property_construction_object_id",
                table: "construction_object_extended_property",
                column: "construction_object_id");

            migrationBuilder.CreateIndex(
                name: "ix_construction_object_extended_property_dictionary_id",
                table: "construction_object_extended_property",
                column: "dictionary_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "construction_object_extended_property");
        }
    }
}
