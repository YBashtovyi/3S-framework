using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00440_UserAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "account_id",
                table: "adm_user");

            migrationBuilder.CreateTable(
                name: "adm_user_account",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_state = table.Column<int>(nullable: false),
                    modified_by = table.Column<Guid>(nullable: false),
                    modified_on = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    auth_provider = table.Column<string>(maxLength: 50, nullable: true),
                    account_id = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adm_user_account", x => x.id);
                    table.ForeignKey(
                        name: "fk_adm_user_account_adm_user_user_id",
                        column: x => x.user_id,
                        principalTable: "adm_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_adm_user_account_user_id",
                table: "adm_user_account",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adm_user_account");

            migrationBuilder.AddColumn<Guid>(
                name: "account_id",
                table: "adm_user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
