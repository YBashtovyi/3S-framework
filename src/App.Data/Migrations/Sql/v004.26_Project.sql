
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201209132345_v004.26_Project') THEN

    ALTER TABLE project ADD description character varying(300) NULL;
    ALTER TABLE project ADD full_name character varying(200) NULL;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201209132345_v004.26_Project', '3.1.9');

    END IF;
END $$;