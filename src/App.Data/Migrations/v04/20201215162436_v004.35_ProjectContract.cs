using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00435_ProjectContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "customer_id",
                table: "prj_contract",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "general_contractor_id",
                table: "prj_contract",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_prj_contract_customer_id",
                table: "prj_contract",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_prj_contract_general_contractor_id",
                table: "prj_contract",
                column: "general_contractor_id");

            migrationBuilder.AddForeignKey(
                name: "fk_prj_contract_organization_customer_id",
                table: "prj_contract",
                column: "customer_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_prj_contract_organization_general_contractor_id",
                table: "prj_contract",
                column: "general_contractor_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prj_contract_organization_customer_id",
                table: "prj_contract");

            migrationBuilder.DropForeignKey(
                name: "fk_prj_contract_organization_general_contractor_id",
                table: "prj_contract");

            migrationBuilder.DropIndex(
                name: "ix_prj_contract_customer_id",
                table: "prj_contract");

            migrationBuilder.DropIndex(
                name: "ix_prj_contract_general_contractor_id",
                table: "prj_contract");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "prj_contract");

            migrationBuilder.DropColumn(
                name: "general_contractor_id",
                table: "prj_contract");
        }
    }
}
