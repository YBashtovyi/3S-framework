using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00400_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cmn_enum_record_cmn_enum_record_parent_id",
                table: "cmn_enum_record");

            migrationBuilder.DropForeignKey(
                name: "fk_org_unit_cmn_enum_record_state_id",
                table: "org_unit");

            migrationBuilder.DropIndex(
                name: "ix_org_unit_state_id",
                table: "org_unit");

            migrationBuilder.DropIndex(
                name: "ix_cmn_enum_record_parent_id",
                table: "cmn_enum_record");

            migrationBuilder.DropColumn(
                name: "state_id",
                table: "org_unit");

            migrationBuilder.DropColumn(
                name: "caption",
                table: "cmn_enum_record");

            migrationBuilder.DropColumn(
                name: "enum_type",
                table: "cmn_enum_record");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "cmn_enum_record");

            migrationBuilder.DropColumn(
                name: "value_type",
                table: "cmn_enum_record");

            migrationBuilder.AddColumn<string>(
                name: "org_unit_type",
                table: "org_unit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "cmn_enum_record",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "group",
                table: "cmn_enum_record",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "item_number",
                table: "cmn_enum_record",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "cmn_enum_record",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "cmn_enum_record",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "org_unit_type",
                table: "org_unit");

            migrationBuilder.DropColumn(
                name: "group",
                table: "cmn_enum_record");

            migrationBuilder.DropColumn(
                name: "item_number",
                table: "cmn_enum_record");

            migrationBuilder.DropColumn(
                name: "name",
                table: "cmn_enum_record");

            migrationBuilder.DropColumn(
                name: "value",
                table: "cmn_enum_record");

            migrationBuilder.AddColumn<Guid>(
                name: "state_id",
                table: "org_unit",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "cmn_enum_record",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "caption",
                table: "cmn_enum_record",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "enum_type",
                table: "cmn_enum_record",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                table: "cmn_enum_record",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "value_type",
                table: "cmn_enum_record",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_org_unit_state_id",
                table: "org_unit",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "ix_cmn_enum_record_parent_id",
                table: "cmn_enum_record",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_enum_record_cmn_enum_record_parent_id",
                table: "cmn_enum_record",
                column: "parent_id",
                principalTable: "cmn_enum_record",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_org_unit_cmn_enum_record_state_id",
                table: "org_unit",
                column: "state_id",
                principalTable: "cmn_enum_record",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
