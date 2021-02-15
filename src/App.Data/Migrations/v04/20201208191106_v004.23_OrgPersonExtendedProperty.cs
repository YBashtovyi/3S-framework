using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00423_OrgPersonExtendedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "property_type",
                table: "org_unit_extended_property");

            migrationBuilder.DropColumn(
                name: "person_property",
                table: "cmn_person_extended_property");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "org_unit_extended_property",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "org_extended_property",
                table: "org_unit_extended_property",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "cmn_person_extended_property",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "json",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "person_extended_property",
                table: "cmn_person_extended_property",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value_json",
                table: "cmn_person_extended_property",
                type: "json",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "org_extended_property",
                table: "org_unit_extended_property");

            migrationBuilder.DropColumn(
                name: "person_extended_property",
                table: "cmn_person_extended_property");

            migrationBuilder.DropColumn(
                name: "value_json",
                table: "cmn_person_extended_property");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "org_unit_extended_property",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "property_type",
                table: "org_unit_extended_property",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "cmn_person_extended_property",
                type: "json",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "person_property",
                table: "cmn_person_extended_property",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
