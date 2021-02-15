using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00409_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adm_user",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(nullable: false),
                    identity_id = table.Column<Guid>(nullable: false),
                    adm_user_state = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adm_user", x => new { x.identity_id, x.employee_id });
                    table.ForeignKey(
                        name: "fk_adm_user_org_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "org_employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_adm_user_employee_id",
                table: "adm_user",
                column: "employee_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adm_user");
        }
    }
}
