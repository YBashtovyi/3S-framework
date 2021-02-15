
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201210150231_v004.30_Department') THEN
    ALTER TABLE org_department DROP CONSTRAINT fk_org_department_organization_organization_id;
    DROP INDEX ix_org_department_organization_id;
    ALTER TABLE org_department DROP COLUMN category;
    ALTER TABLE org_department DROP COLUMN location;
    ALTER TABLE org_department DROP COLUMN organization_id;
    ALTER TABLE org_department DROP COLUMN zip_code;
    ALTER TABLE org_department ADD caption text NULL;
    ALTER TABLE org_department ADD name text NULL;
    CREATE INDEX ix_org_department_parent_id ON org_department (parent_id);
    ALTER TABLE org_department ADD CONSTRAINT fk_org_department_org_unit_parent_id FOREIGN KEY (parent_id) REFERENCES org_unit (id) ON DELETE RESTRICT;
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201210150231_v004.30_Department', '3.1.9');
    END IF;
END $$;
