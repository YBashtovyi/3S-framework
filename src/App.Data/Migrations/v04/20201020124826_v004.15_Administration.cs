using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00415_Administration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_application_row_level_right");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_sys_user_default_value_user_id_entity_name",
                table: "sys_user_default_value");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_sys_row_level_right_profile_id_entity_name_main_entity_name",
                table: "sys_row_level_right");

            migrationBuilder.DropColumn(
                name: "adm_user_state",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "identity_id",
                table: "adm_user");

            migrationBuilder.AddColumn<string>(
                name: "discriminator",
                table: "org_employee",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "account_id",
                table: "adm_user",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "adm_user",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rls",
                table: "adm_user",
                type: "json",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "roles",
                table: "adm_user",
                type: "json",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "adm_right",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    right_type = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    code = table.Column<string>(maxLength: 50, nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: false),
                    els = table.Column<string>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adm_right", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "adm_role",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    code = table.Column<string>(maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: false),
                    rls = table.Column<string>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adm_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "adm_role_right",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    role_id = table.Column<Guid>(nullable: false),
                    right_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adm_role_right", x => x.id);
                    table.ForeignKey(
                        name: "fk_adm_role_right_adm_right_right_id",
                        column: x => x.right_id,
                        principalTable: "adm_right",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_adm_role_right_adm_role_role_id",
                        column: x => x.role_id,
                        principalTable: "adm_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_sys_user_default_value_user_id",
                table: "sys_user_default_value",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_sys_row_level_right_profile_id",
                table: "sys_row_level_right",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_adm_role_right_right_id",
                table: "adm_role_right",
                column: "right_id");

            migrationBuilder.CreateIndex(
                name: "ix_adm_role_right_role_id",
                table: "adm_role_right",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adm_role_right");

            migrationBuilder.DropTable(
                name: "adm_right");

            migrationBuilder.DropTable(
                name: "adm_role");

            migrationBuilder.DropIndex(
                name: "ix_sys_user_default_value_user_id",
                table: "sys_user_default_value");

            migrationBuilder.DropIndex(
                name: "ix_sys_row_level_right_profile_id",
                table: "sys_row_level_right");

            migrationBuilder.DropColumn(
                name: "discriminator",
                table: "org_employee");

            migrationBuilder.DropColumn(
                name: "account_id",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "login",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "rls",
                table: "adm_user");

            migrationBuilder.DropColumn(
                name: "roles",
                table: "adm_user");

            migrationBuilder.AddColumn<string>(
                name: "adm_user_state",
                table: "adm_user",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "identity_id",
                table: "adm_user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "ak_sys_user_default_value_user_id_entity_name",
                table: "sys_user_default_value",
                columns: new[] { "user_id", "entity_name" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_sys_row_level_right_profile_id_entity_name_main_entity_name",
                table: "sys_row_level_right",
                columns: new[] { "profile_id", "entity_name", "main_entity_name" });

            migrationBuilder.CreateTable(
                name: "sys_application_row_level_right",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    caption = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    entity_name = table.Column<string>(type: "text", nullable: false),
                    hierarchy_field_name = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_hierarchical = table.Column<bool>(type: "boolean", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    record_state = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_application_row_level_right", x => x.id);
                });
        }
    }
}
