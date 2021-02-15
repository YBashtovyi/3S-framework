
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200915155056_v004.02_OrgDepartment') THEN
    ALTER TABLE org_department DROP CONSTRAINT fk_org_department_cmn_enum_record_department_type_id;
    DROP INDEX ix_org_department_department_type_id;
    ALTER TABLE org_department DROP COLUMN department_type_id;
    ALTER TABLE org_department ADD department_type text NULL;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200915155056_v004.02_OrgDepartment', '3.1.7');
    END IF;
END $$;
