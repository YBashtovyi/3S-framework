using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00410_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_adm_user",
                table: "adm_user");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "adm_user",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "adm_user",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                table: "adm_user",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modified_by",
                table: "adm_user",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_on",
                table: "adm_user",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "record_state",
                table: "adm_user",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_adm_user",
                table: "adm_user",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_adm_user",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "id",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "created_on",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "modified_on",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "record_state",
                table: "adm_user");

            migrationBuilder.AddPrimaryKey(
                name: "pk_adm_user",
                table: "adm_user",
                columns: new[] { "identity_id", "employee_id" });
        }
    }
}
