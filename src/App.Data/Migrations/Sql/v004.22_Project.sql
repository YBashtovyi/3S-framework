
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201207145323_v004.22_Project') THEN
    DROP INDEX ix_project_owner_id_const_obj_number;
    ALTER TABLE project DROP COLUMN const_obj_number;

    ALTER TABLE construction_object_extended_property ALTER COLUMN value TYPE character varying(200);
    ALTER TABLE construction_object_extended_property ALTER COLUMN value SET NOT NULL;
    ALTER TABLE construction_object_extended_property ALTER COLUMN value DROP DEFAULT;

    CREATE INDEX ix_project_owner_id ON project (owner_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201207145323_v004.22_Project', '3.1.9');
    END IF;
END $$;