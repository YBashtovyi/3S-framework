using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00422_Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_project_owner_id_const_obj_number",
                table: "project");

            migrationBuilder.DropColumn(
                name: "const_obj_number",
                table: "project");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "construction_object_extended_property",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_project_owner_id",
                table: "project",
                column: "owner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_project_owner_id",
                table: "project");

            migrationBuilder.AddColumn<string>(
                name: "const_obj_number",
                table: "project",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "construction_object_extended_property",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "ix_project_owner_id_const_obj_number",
                table: "project",
                columns: new[] { "owner_id", "const_obj_number" },
                unique: true);
        }
    }
}
