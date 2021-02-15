
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201209143248_v004.28_Project_Coordinates') THEN
    ALTER TABLE project ADD atu_coordinates json NULL;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201209143248_v004.28_Project_Coordinates', '3.1.9');

    END IF;
END $$;