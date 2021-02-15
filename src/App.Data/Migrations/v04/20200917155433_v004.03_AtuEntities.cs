using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00403_AtuEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_atu_city_cmn_enum_record_type_id",
                table: "atu_city");

            migrationBuilder.DropTable(
                name: "atu_city_district");

            migrationBuilder.DropIndex(
                name: "ix_atu_city_type_id",
                table: "atu_city");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cdn_coordinate",
                table: "cdn_coordinate");

            migrationBuilder.DropColumn(
                name: "caption",
                table: "atu_region");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "atu_region");

            migrationBuilder.DropColumn(
                name: "koatuu",
                table: "atu_region");

            migrationBuilder.DropColumn(
                name: "caption",
                table: "atu_city");

            migrationBuilder.DropColumn(
                name: "code",
                table: "atu_city");

            migrationBuilder.DropColumn(
                name: "region_id",
                table: "atu_city");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "atu_city");

            migrationBuilder.RenameTable(
                name: "cdn_coordinate",
                newName: "atu_coordinate");

            migrationBuilder.AddColumn<string>(
                name: "atu_street_type",
                table: "atu_street",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "atu_street",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "parent_id",
                table: "atu_region",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "atu_region",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "atu_region_type",
                table: "atu_region",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "koatu",
                table: "atu_region",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "atu_region",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "atu_city_type",
                table: "atu_city",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "atu_city",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_atu_coordinate",
                table: "atu_coordinate",
                column: "id");

            migrationBuilder.CreateTable(
                name: "atu_address",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    street_id = table.Column<Guid>(nullable: false),
                    zip_code = table.Column<string>(maxLength: 50, nullable: true),
                    building = table.Column<string>(maxLength: 50, nullable: true),
                    entrance = table.Column<string>(maxLength: 50, nullable: true),
                    apartment = table.Column<string>(maxLength: 50, nullable: true),
                    comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atu_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_atu_address_atu_street_street_id",
                        column: x => x.street_id,
                        principalTable: "atu_street",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "atu_district",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    code = table.Column<string>(maxLength: 100, nullable: true),
                    parent_id = table.Column<Guid>(nullable: false),
                    atu_district_type = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atu_district", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "atu_subject",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    derived_entity = table.Column<string>(nullable: true),
                    name = table.Column<string>(maxLength: 200, nullable: true),
                    code = table.Column<string>(maxLength: 100, nullable: true),
                    parent_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atu_subject", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "atu_city_district_streets_map",
                columns: table => new
                {
                    city_id = table.Column<Guid>(nullable: false),
                    district_id = table.Column<Guid>(nullable: false),
                    street_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atu_city_district_streets_map", x => new { x.city_id, x.district_id, x.street_id });
                    table.ForeignKey(
                        name: "fk_atu_city_district_streets_map_atu_city_city_id",
                        column: x => x.city_id,
                        principalTable: "atu_city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_atu_city_district_streets_map_atu_district_district_id",
                        column: x => x.district_id,
                        principalTable: "atu_district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_atu_city_district_streets_map_atu_street_street_id",
                        column: x => x.street_id,
                        principalTable: "atu_street",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_atu_address_street_id",
                table: "atu_address",
                column: "street_id");

            migrationBuilder.CreateIndex(
                name: "ix_atu_city_district_streets_map_district_id",
                table: "atu_city_district_streets_map",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "ix_atu_city_district_streets_map_street_id",
                table: "atu_city_district_streets_map",
                column: "street_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atu_address");

            migrationBuilder.DropTable(
                name: "atu_city_district_streets_map");

            migrationBuilder.DropTable(
                name: "atu_subject");

            migrationBuilder.DropTable(
                name: "atu_district");

            migrationBuilder.DropPrimaryKey(
                name: "pk_atu_coordinate",
                table: "atu_coordinate");

            migrationBuilder.DropColumn(
                name: "atu_street_type",
                table: "atu_street");

            migrationBuilder.DropColumn(
                name: "name",
                table: "atu_street");

            migrationBuilder.DropColumn(
                name: "atu_region_type",
                table: "atu_region");

            migrationBuilder.DropColumn(
                name: "koatu",
                table: "atu_region");

            migrationBuilder.DropColumn(
                name: "name",
                table: "atu_region");

            migrationBuilder.DropColumn(
                name: "atu_city_type",
                table: "atu_city");

            migrationBuilder.DropColumn(
                name: "name",
                table: "atu_city");

            migrationBuilder.RenameTable(
                name: "atu_coordinate",
                newName: "cdn_coordinate");

            migrationBuilder.AlterColumn<Guid>(
                name: "parent_id",
                table: "atu_region",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "atu_region",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "caption",
                table: "atu_region",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "country_id",
                table: "atu_region",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "koatuu",
                table: "atu_region",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "caption",
                table: "atu_city",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "atu_city",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "region_id",
                table: "atu_city",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "type_id",
                table: "atu_city",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_cdn_coordinate",
                table: "cdn_coordinate",
                column: "id");

            migrationBuilder.CreateTable(
                name: "atu_city_district",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    caption = table.Column<string>(type: "text", nullable: true),
                    city_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    record_state = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atu_city_district", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_atu_city_type_id",
                table: "atu_city",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_atu_city_cmn_enum_record_type_id",
                table: "atu_city",
                column: "type_id",
                principalTable: "cmn_enum_record",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
