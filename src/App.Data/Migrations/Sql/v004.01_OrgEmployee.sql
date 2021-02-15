
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200915153832_v004.01_OrgEmployee') THEN
    ALTER TABLE org_employee DROP CONSTRAINT fk_org_employee_cmn_enum_record_position_type_id;
    DROP INDEX ix_org_employee_position_type_id;
    ALTER TABLE org_employee DROP COLUMN department_section_id;
    ALTER TABLE org_employee DROP COLUMN position_type_id;
    ALTER TABLE org_employee DROP COLUMN working_start_date;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200915153832_v004.01_OrgEmployee', '3.1.7');
    END IF;
END $$;