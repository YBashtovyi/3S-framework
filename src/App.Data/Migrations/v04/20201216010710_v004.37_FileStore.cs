using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00437_FileStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cmn_file_store_cmn_enum_record_document_type_id",
                table: "cmn_file_store");

            migrationBuilder.DropForeignKey(
                name: "fk_cmn_file_store_cmn_enum_record_file_group_id",
                table: "cmn_file_store");

            migrationBuilder.DropIndex(
                name: "ix_cmn_file_store_document_type_id",
                table: "cmn_file_store");

            migrationBuilder.DropIndex(
                name: "ix_cmn_file_store_file_group_id",
                table: "cmn_file_store");

            migrationBuilder.DropColumn(
                name: "document_type_id",
                table: "cmn_file_store");

            migrationBuilder.DropColumn(
                name: "file_group_id",
                table: "cmn_file_store");

            migrationBuilder.AlterColumn<string>(
                name: "file_type",
                table: "cmn_file_store",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "file_path",
                table: "cmn_file_store",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "file_name",
                table: "cmn_file_store",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "entity_name",
                table: "cmn_file_store",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "cmn_file_store",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content_type",
                table: "cmn_file_store",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "owner_id",
                table: "cmn_file_store",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "type_of_attached_file",
                table: "cmn_file_store",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_cmn_file_store_owner_id",
                table: "cmn_file_store",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_file_store_org_unit_owner_id",
                table: "cmn_file_store",
                column: "owner_id",
                principalTable: "org_unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cmn_file_store_org_unit_owner_id",
                table: "cmn_file_store");

            migrationBuilder.DropIndex(
                name: "ix_cmn_file_store_owner_id",
                table: "cmn_file_store");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "cmn_file_store");

            migrationBuilder.DropColumn(
                name: "type_of_attached_file",
                table: "cmn_file_store");

            migrationBuilder.AlterColumn<string>(
                name: "file_type",
                table: "cmn_file_store",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "file_path",
                table: "cmn_file_store",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "file_name",
                table: "cmn_file_store",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "entity_name",
                table: "cmn_file_store",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "cmn_file_store",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content_type",
                table: "cmn_file_store",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "document_type_id",
                table: "cmn_file_store",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "file_group_id",
                table: "cmn_file_store",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_cmn_file_store_document_type_id",
                table: "cmn_file_store",
                column: "document_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_cmn_file_store_file_group_id",
                table: "cmn_file_store",
                column: "file_group_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_file_store_cmn_enum_record_document_type_id",
                table: "cmn_file_store",
                column: "document_type_id",
                principalTable: "cmn_enum_record",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_file_store_cmn_enum_record_file_group_id",
                table: "cmn_file_store",
                column: "file_group_id",
                principalTable: "cmn_enum_record",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
