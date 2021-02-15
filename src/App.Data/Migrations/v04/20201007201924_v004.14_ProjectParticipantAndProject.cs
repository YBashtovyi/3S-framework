using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00414_ProjectParticipantAndProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_project",
                table: "project");

            migrationBuilder.AlterColumn<string>(
                name: "const_obj_number",
                table: "project",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "pk_project",
                table: "project",
                column: "id");

            migrationBuilder.CreateTable(
                name: "prj_participant",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    project_id = table.Column<Guid>(nullable: false),
                    project_role = table.Column<string>(maxLength: 50, nullable: false),
                    participant_id = table.Column<Guid>(nullable: false),
                    responsible_person_id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prj_participant", x => x.id);
                    table.ForeignKey(
                        name: "fk_prj_participant_organization_participant_id",
                        column: x => x.participant_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prj_participant_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prj_participant_org_employee_responsible_person_id",
                        column: x => x.responsible_person_id,
                        principalTable: "org_employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_owner_id_const_obj_number",
                table: "project",
                columns: new[] { "owner_id", "const_obj_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_prj_participant_participant_id",
                table: "prj_participant",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_participant_project_id",
                table: "prj_participant",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_participant_responsible_person_id",
                table: "prj_participant",
                column: "responsible_person_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prj_participant");

            migrationBuilder.DropPrimaryKey(
                name: "pk_project",
                table: "project");

            migrationBuilder.DropIndex(
                name: "ix_project_owner_id_const_obj_number",
                table: "project");

            migrationBuilder.AlterColumn<string>(
                name: "const_obj_number",
                table: "project",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_project",
                table: "project",
                columns: new[] { "owner_id", "const_obj_number" });
        }
    }
}
