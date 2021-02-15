
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201209130636_v004.25_TypeOfProjectWork') THEN
    ALTER TABLE cdn_type_of_project_work ALTER COLUMN parent_id TYPE uuid;
    ALTER TABLE cdn_type_of_project_work ALTER COLUMN parent_id DROP NOT NULL;
    ALTER TABLE cdn_type_of_project_work ALTER COLUMN parent_id DROP DEFAULT;

    ALTER TABLE cdn_type_of_project_work ALTER COLUMN name TYPE character varying(200);
    ALTER TABLE cdn_type_of_project_work ALTER COLUMN name SET NOT NULL;
    ALTER TABLE cdn_type_of_project_work ALTER COLUMN name DROP DEFAULT;

    ALTER TABLE cdn_type_of_project_work ALTER COLUMN full_name TYPE character varying(300);
    ALTER TABLE cdn_type_of_project_work ALTER COLUMN full_name SET NOT NULL;
    ALTER TABLE cdn_type_of_project_work ALTER COLUMN full_name DROP DEFAULT;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201209130636_v004.25_TypeOfProjectWork', '3.1.9');
    END IF;
END $$;