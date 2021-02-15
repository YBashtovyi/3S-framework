
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201209121900_v004.24_Project') THEN
    ALTER TABLE project ADD repair_length character varying(20) NULL;
    ALTER TABLE project ADD repair_square character varying(20) NULL;
    
    CREATE TABLE cdn_type_of_project_work (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        code character varying(50) NOT NULL,
        name character varying(100) NOT NULL,
        full_name character varying(200) NOT NULL,
        parent_id uuid NULL,
        CONSTRAINT pk_cdn_type_of_project_work PRIMARY KEY (id)
    );

    INSERT INTO "cdn_type_of_project_work" ("id", "record_state", "modified_by", "created_by", "created_on", "code", "name", "full_name", "parent_id")
    VALUES ('f38eda5d-7376-41fd-b55b-8de9ba49067a', 2, '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '0001-01-01 00:00:00',
    '45000000-7', 'Будівельні роботи', 'Будівельні роботи', null);

    ALTER TABLE project ADD type_of_project_work_id uuid NOT NULL;
    
    UPDATE "project" SET type_of_project_work_id = 'f38eda5d-7376-41fd-b55b-8de9ba49067a';

    CREATE INDEX ix_project_type_of_project_work_id ON project (type_of_project_work_id);
    ALTER TABLE project ADD CONSTRAINT fk_project_cdn_type_of_project_work_type_of_project_work_id FOREIGN KEY (type_of_project_work_id) REFERENCES cdn_type_of_project_work (id) ON DELETE CASCADE;
    
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201209121900_v004.24_Project', '3.1.9');

    END IF;
END $$;
