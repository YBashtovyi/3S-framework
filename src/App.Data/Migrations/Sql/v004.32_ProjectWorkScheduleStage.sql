
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201212193145_v004.32_ProjectWorkScheduleStage') THEN
    ALTER TABLE prj_work_schedule_stage ADD stage_name character varying(200) NOT NULL DEFAULT '';
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201212193145_v004.32_ProjectWorkScheduleStage', '3.1.9');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201212193145_v004.32_ProjectWorkScheduleStage') THEN
    
    END IF;
END $$;
