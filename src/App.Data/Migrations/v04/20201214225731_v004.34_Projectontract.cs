using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00434_Projectontract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "prj_contract",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    project_id = table.Column<Guid>(nullable: false),
                    doc_type = table.Column<string>(maxLength: 50, nullable: false),
                    reg_date = table.Column<DateTime>(nullable: false),
                    reg_number = table.Column<string>(maxLength: 100, nullable: false),
                    doc_state = table.Column<string>(maxLength: 50, nullable: false),
                    cost = table.Column<decimal>(nullable: false),
                    bidding_type = table.Column<string>(maxLength: 50, nullable: false),
                    tender_code = table.Column<string>(maxLength: 100, nullable: true),
                    bidding_code = table.Column<Guid>(nullable: true),
                    bidding_subject = table.Column<string>(maxLength: 2000, nullable: true),
                    description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prj_contract", x => x.id);
                    table.ForeignKey(
                        name: "fk_prj_contract_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_prj_contract_project_id",
                table: "prj_contract",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prj_contract");
        }
    }
}
