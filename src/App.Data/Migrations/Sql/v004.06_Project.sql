
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200924201805_v004.06_Project') THEN
    ALTER TABLE project DROP COLUMN implementation_state;
    ALTER TABLE project ADD project_implementation_state character varying(50) NOT NULL DEFAULT '';

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200924201805_v004.06_Project', '3.1.7');
    END IF;
END $$;