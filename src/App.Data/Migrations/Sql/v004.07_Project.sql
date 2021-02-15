
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200924203244_v004.07_Project') THEN
    ALTER TABLE project DROP CONSTRAINT fk_project_atu_district_district_id;

    ALTER TABLE project ALTER COLUMN district_id TYPE uuid;
    ALTER TABLE project ALTER COLUMN district_id DROP NOT NULL;
    ALTER TABLE project ALTER COLUMN district_id DROP DEFAULT;

    ALTER TABLE project ALTER COLUMN date_end TYPE timestamp without time zone;
    ALTER TABLE project ALTER COLUMN date_end DROP NOT NULL;
    ALTER TABLE project ALTER COLUMN date_end DROP DEFAULT;

    ALTER TABLE project ALTER COLUMN date_begin TYPE timestamp without time zone;
    ALTER TABLE project ALTER COLUMN date_begin DROP NOT NULL;
    ALTER TABLE project ALTER COLUMN date_begin DROP DEFAULT;

    ALTER TABLE project ALTER COLUMN cost TYPE numeric;
    ALTER TABLE project ALTER COLUMN cost DROP NOT NULL;
    ALTER TABLE project ALTER COLUMN cost DROP DEFAULT;

    ALTER TABLE project ADD CONSTRAINT fk_project_atu_district_district_id FOREIGN KEY (district_id) REFERENCES atu_district (id) ON DELETE RESTRICT;

     INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200924203244_v004.07_Project', '3.1.7');

    END IF;
END $$;
