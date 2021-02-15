
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201211161620_v004.31_ProjectWorkSchedule') THEN
    ALTER TABLE cdn_work_sub_type ADD parent_id uuid NULL;
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201211161620_v004.31_ProjectWorkSchedule', '3.1.9');
    END IF;
END $$;
