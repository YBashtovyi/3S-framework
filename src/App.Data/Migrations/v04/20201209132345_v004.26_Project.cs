using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00426_Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "project",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "full_name",
                table: "project",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "project");

            migrationBuilder.DropColumn(
                name: "full_name",
                table: "project");
        }
    }
}
