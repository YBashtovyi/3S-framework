using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00418_ConstructionObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "construction_object",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    code = table.Column<string>(maxLength: 20, nullable: false),
                    object_status = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    full_name = table.Column<string>(maxLength: 100, nullable: false),
                    type_of_construction_object = table.Column<string>(maxLength: 50, nullable: false),
                    class_of_consequence = table.Column<string>(maxLength: 50, nullable: false),
                    atu_coordinates = table.Column<string>(type: "json", nullable: true),
                    description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_construction_object", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "construction_object");
        }
    }
}
