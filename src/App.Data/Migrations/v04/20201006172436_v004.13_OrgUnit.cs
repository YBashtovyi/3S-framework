using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class v00413_OrgUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cmn_notification_organization_organization_id",
                table: "cmn_notification");

            migrationBuilder.DropForeignKey(
                name: "fk_cmn_notification_receiver_organization_organization_id",
                table: "cmn_notification_receiver");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_resource_organization_organization_id",
                table: "eq_schedule_resource");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_setting_organization_organization_id",
                table: "eq_schedule_setting");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_setting_property_organization_organization_~",
                table: "eq_schedule_setting_property");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_slot_organization_organization_id",
                table: "eq_schedule_slot");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_time_organization_organization_id",
                table: "eq_schedule_time");

            migrationBuilder.DropForeignKey(
                name: "fk_org_department_organization_organization_id",
                table: "org_department");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organization",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "org_unit_type",
                table: "org_unit");

            migrationBuilder.RenameTable(
                name: "organization",
                newName: "organization");

            migrationBuilder.RenameIndex(
                name: "ix_organization_code",
                table: "organization",
                newName: "ix_organization_code");

            migrationBuilder.AddPrimaryKey(
                name: "pk_organization",
                table: "organization",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_notification_organization_organization_id",
                table: "cmn_notification",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_notification_receiver_organization_organization_id",
                table: "cmn_notification_receiver",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_resource_organization_organization_id",
                table: "eq_schedule_resource",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_setting_organization_organization_id",
                table: "eq_schedule_setting",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_setting_property_organization_organization_id",
                table: "eq_schedule_setting_property",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_slot_organization_organization_id",
                table: "eq_schedule_slot",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_time_organization_organization_id",
                table: "eq_schedule_time",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_org_department_organization_organization_id",
                table: "org_department",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cmn_notification_organization_organization_id",
                table: "cmn_notification");

            migrationBuilder.DropForeignKey(
                name: "fk_cmn_notification_receiver_organization_organization_id",
                table: "cmn_notification_receiver");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_resource_organization_organization_id",
                table: "eq_schedule_resource");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_setting_organization_organization_id",
                table: "eq_schedule_setting");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_setting_property_organization_organization_id",
                table: "eq_schedule_setting_property");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_slot_organization_organization_id",
                table: "eq_schedule_slot");

            migrationBuilder.DropForeignKey(
                name: "fk_eq_schedule_time_organization_organization_id",
                table: "eq_schedule_time");

            migrationBuilder.DropForeignKey(
                name: "fk_org_department_organization_organization_id",
                table: "org_department");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organization",
                table: "organization");

            migrationBuilder.RenameTable(
                name: "organization",
                newName: "organization");

            migrationBuilder.RenameIndex(
                name: "ix_organization_code",
                table: "organization",
                newName: "ix_organization_code");

            migrationBuilder.AddColumn<string>(
                name: "org_unit_type",
                table: "org_unit",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_organization",
                table: "organization",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_notification_organization_organization_id",
                table: "cmn_notification",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmn_notification_receiver_organization_organization_id",
                table: "cmn_notification_receiver",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_resource_organization_organization_id",
                table: "eq_schedule_resource",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_setting_organization_organization_id",
                table: "eq_schedule_setting",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_setting_property_organization_organization_~",
                table: "eq_schedule_setting_property",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_slot_organization_organization_id",
                table: "eq_schedule_slot",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_eq_schedule_time_organization_organization_id",
                table: "eq_schedule_time",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_org_department_organization_organization_id",
                table: "org_department",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
