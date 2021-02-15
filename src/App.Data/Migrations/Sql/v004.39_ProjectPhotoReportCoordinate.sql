
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201217080010_v004.39_ProjectPhotoReportCoordinate') THEN
    ALTER TABLE prj_photo_report ADD atu_coordinates json NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201217080010_v004.39_ProjectPhotoReportCoordinate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201217080010_v004.39_ProjectPhotoReportCoordinate', '3.1.9');
    END IF;
END $$;
