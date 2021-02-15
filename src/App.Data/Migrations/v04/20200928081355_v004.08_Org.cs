using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00408_Org : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cmn_person_cmn_enum_record_gender_id",
                table: "cmn_person");

            migrationBuilder.DropForeignKey(
                name: "fk_cmn_person_org_organization_owner_id",
                table: "cmn_person");

            migrationBuilder.DropForeignKey(
                name: "fk_org_employee_org_department_department_id",
                table: "org_employee");

            migrationBuilder.DropForeignKey(
                name: "fk_org_employee_org_organization_organization_id",
                table: "org_employee");

            migrationBuilder.DropIndex(
                name: "ix_org_employee_department_id",
                table: "org_employee");

            migrationBuilder.DropIndex(
                name: "ix_org_employee_organization_id",
                table: "org_employee");

            migrationBuilder.DropIndex(
                name: "ix_cmn_person_gender_id",
                table: "cmn_person");

            migrationBuilder.DropIndex(
                name: "ix_cmn_person_owner_id",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "base_entity",
                table: "org_organization");

            migrationBuilder.DropColumn(
                name: "category",
                table: "org_organization");

            migrationBuilder.DropColumn(
                name: "caption",
                table: "org_employee");

            migrationBuilder.DropColumn(
                name: "department_id",
                table: "org_employee");

            migrationBuilder.DropColumn(
                name: "organization_id",
                table: "org_employee");

            migrationBuilder.DropColumn(
                name: "description",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "email",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "gender_id",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "ipn",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "location",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "name",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "no_ipn",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "photo",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "subscribed_to_notifications",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "cmn_person");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "org_organization",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "org_organization",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "org_organization",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "edrpou",
                table: "org_organization",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "org_organization",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "org_type",
                table: "org_organization",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "middle_name",
                table: "cmn_person",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "cmn_person",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "cmn_person",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "cmn_person",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "identity_document",
                table: "cmn_person",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "no_tax_number",
                table: "cmn_person",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "tax_number",
                table: "cmn_person",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "cdn_position",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    code = table.Column<string>(maxLength: 50, nullable: true),
                    name = table.Column<string>(maxLength: 100, nullable: true),
                    caption = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cdn_position", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cmn_person_extended_property",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    person_id = table.Column<Guid>(nullable: false),
                    person_property = table.Column<string>(maxLength: 50, nullable: true),
                    value = table.Column<string>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmn_person_extended_property", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "org_unit_atu_address",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    org_unit_id = table.Column<Guid>(nullable: true),
                    atu_address_id = table.Column<Guid>(nullable: true),
                    address_type = table.Column<string>(maxLength: 50, nullable: true),
                    comment = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_org_unit_atu_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_org_unit_atu_address_atu_address_atu_address_id",
                        column: x => x.atu_address_id,
                        principalTable: "atu_address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_org_unit_atu_address_org_unit_org_unit_id",
                        column: x => x.org_unit_id,
                        principalTable: "org_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "org_unit_extended_property",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    org_unit_id = table.Column<Guid>(nullable: false),
                    property_type = table.Column<string>(maxLength: 50, nullable: true),
                    value = table.Column<string>(maxLength: 100, nullable: true),
                    value_json = table.Column<string>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_org_unit_extended_property", x => x.id);
                    table.ForeignKey(
                        name: "fk_org_unit_extended_property_org_unit_org_unit_id",
                        column: x => x.org_unit_id,
                        principalTable: "org_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "org_unit_position",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    org_unit_id = table.Column<Guid>(nullable: false),
                    position_id = table.Column<Guid>(nullable: false),
                    staff_unit_count = table.Column<decimal>(nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_org_unit_position", x => x.id);
                    table.ForeignKey(
                        name: "fk_org_unit_position_org_unit_org_unit_id",
                        column: x => x.org_unit_id,
                        principalTable: "org_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_org_unit_position_cdn_position_position_id",
                        column: x => x.position_id,
                        principalTable: "cdn_position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "org_unit_staff",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    org_unit_position_id = table.Column<Guid>(nullable: false),
                    employee_id = table.Column<Guid>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_org_unit_staff", x => x.id);
                    table.ForeignKey(
                        name: "fk_org_unit_staff_org_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "org_employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_org_unit_staff_org_unit_position_org_unit_position_id",
                        column: x => x.org_unit_position_id,
                        principalTable: "org_unit_position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_org_organization_code",
                table: "org_organization",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_org_unit_atu_address_atu_address_id",
                table: "org_unit_atu_address",
                column: "atu_address_id");

            migrationBuilder.CreateIndex(
                name: "ix_org_unit_atu_address_org_unit_id",
                table: "org_unit_atu_address",
                column: "org_unit_id");

            migrationBuilder.CreateIndex(
                name: "ix_org_unit_extended_property_org_unit_id",
                table: "org_unit_extended_property",
                column: "org_unit_id");

            migrationBuilder.CreateIndex(
                name: "ix_org_unit_position_org_unit_id",
                table: "org_unit_position",
                column: "org_unit_id");

            migrationBuilder.CreateIndex(
                name: "ix_org_unit_position_position_id",
                table: "org_unit_position",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_org_unit_staff_employee_id",
                table: "org_unit_staff",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_org_unit_staff_org_unit_position_id",
                table: "org_unit_staff",
                column: "org_unit_position_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cmn_person_extended_property");

            migrationBuilder.DropTable(
                name: "org_unit_atu_address");

            migrationBuilder.DropTable(
                name: "org_unit_extended_property");

            migrationBuilder.DropTable(
                name: "org_unit_staff");

            migrationBuilder.DropTable(
                name: "org_unit_position");

            migrationBuilder.DropTable(
                name: "cdn_position");

            migrationBuilder.DropIndex(
                name: "ix_org_organization_code",
                table: "org_organization");

            migrationBuilder.DropColumn(
                name: "edrpou",
                table: "org_organization");

            migrationBuilder.DropColumn(
                name: "name",
                table: "org_organization");

            migrationBuilder.DropColumn(
                name: "org_type",
                table: "org_organization");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "identity_document",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "no_tax_number",
                table: "cmn_person");

            migrationBuilder.DropColumn(
                name: "tax_number",
                table: "cmn_person");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "org_organization",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "org_organization",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "org_organization",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "base_entity",
                table: "org_organization",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "org_organization",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "caption",
                table: "org_employee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "department_id",
                table: "org_employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "organization_id",
                table: "org_employee",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "middle_name",
                table: "cmn_person",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "cmn_person",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "cmn_person",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "cmn_person",
                type: "character varying(129)",
                maxLength: 129,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "gender_id",
                table: "cmn_person",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipn",
                table: "cmn_person",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "cmn_person",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "cmn_person",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "no_ipn",
                table: "cmn_person",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "owner_id",
                table: "cmn_person",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "cmn_person",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "cmn_person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "subscribed_to_notifications",
                table: "cmn_person",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "cmn_person",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_org_employee_department_id",
                table: "org_employee",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_org_employee_organization_id",
                table: "org_employee",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_cmn_person_gender_id",
                table: "cmn_person",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "ix_cmn_person_owner_id",
                table: "cmn_person",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_person_cmn_enum_record_gender_id",
                table: "cmn_person",
                column: "gender_id",
                principalTable: "cmn_enum_record",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_person_org_organization_owner_id",
                table: "cmn_person",
                column: "owner_id",
                principalTable: "org_organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_org_employee_org_department_department_id",
                table: "org_employee",
                column: "department_id",
                principalTable: "org_department",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_org_employee_org_organization_organization_id",
                table: "org_employee",
                column: "organization_id",
                principalTable: "org_organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
