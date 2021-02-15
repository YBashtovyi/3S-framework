
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201213175806_v004.33_ProjectWorkScheduleSubType') THEN

    ALTER TABLE prj_work_schedule_sub_type DROP CONSTRAINT "fk_prj_work_schedule_sub_type_prj_work_schedule_stage_prj_proj~";
    DROP INDEX "ix_prj_work_schedule_sub_type_prj_project_work_schedule_stage_~";
    ALTER TABLE prj_work_schedule_sub_type DROP COLUMN prj_project_work_schedule_stage_id;
    CREATE INDEX ix_prj_work_schedule_sub_type_prj_work_schedule_stage_id ON prj_work_schedule_sub_type (prj_work_schedule_stage_id);
    ALTER TABLE prj_work_schedule_sub_type ADD CONSTRAINT "fk_prj_work_schedule_sub_type_prj_work_schedule_stage_prj_work~" FOREIGN KEY (prj_work_schedule_stage_id) REFERENCES prj_work_schedule_stage (id) ON DELETE CASCADE;
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201213175806_v004.33_ProjectWorkScheduleSubType', '3.1.9');

    END IF;
END $$;