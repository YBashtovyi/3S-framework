
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201209141435_v004.27_Project') THEN
    ALTER TABLE project ALTER COLUMN date_end TYPE timestamp without time zone;
    ALTER TABLE project ALTER COLUMN date_end SET NOT NULL;
    ALTER TABLE project ALTER COLUMN date_end DROP DEFAULT;

    ALTER TABLE project ALTER COLUMN date_begin TYPE timestamp without time zone;
    ALTER TABLE project ALTER COLUMN date_begin SET NOT NULL;
    ALTER TABLE project ALTER COLUMN date_begin DROP DEFAULT;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201209141435_v004.27_Project', '3.1.9');
    END IF;
END $$;