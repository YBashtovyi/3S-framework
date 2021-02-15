using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00420_ConstructionObjectExPropertyDictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "construction_object_ex_property_dictionary",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    code = table.Column<string>(maxLength: 32, nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    full_name = table.Column<string>(maxLength: 300, nullable: false),
                    data_format = table.Column<string>(maxLength: 50, nullable: false),
                    parent_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_construction_object_ex_property_dictionary", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "construction_object_ex_property_dictionary");
        }
    }
}
