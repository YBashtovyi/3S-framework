
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201211160210_v004.30_ProjectWorkSchedule') THEN
    CREATE TABLE cdn_work_sub_type (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        code character varying(50) NOT NULL,
        name character varying(200) NOT NULL,
        measurement_unit character varying(50) NOT NULL,
        classifier_type character varying(50) NOT NULL,
        is_active boolean NOT NULL,
        CONSTRAINT pk_cdn_work_sub_type PRIMARY KEY (id)
    );

    CREATE TABLE prj_work_schedule (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        project_id uuid NOT NULL,
        doc_type character varying(50) NOT NULL,
        parent_id uuid NULL,
        reg_date timestamp without time zone NOT NULL,
        reg_number character varying(100) NOT NULL,
        doc_state character varying(50) NOT NULL,
        description character varying(500) NULL,
        CONSTRAINT pk_prj_work_schedule PRIMARY KEY (id),
        CONSTRAINT fk_prj_work_schedule_cmn_document_parent_id FOREIGN KEY (parent_id) REFERENCES cmn_document (id) ON DELETE RESTRICT,
        CONSTRAINT fk_prj_work_schedule_project_project_id FOREIGN KEY (project_id) REFERENCES project (id) ON DELETE CASCADE
    );

    CREATE TABLE prj_work_schedule_stage (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        prj_work_schedule_id uuid NOT NULL,
        stage_number character varying(50) NOT NULL,
        begin_date timestamp without time zone NOT NULL,
        end_date timestamp without time zone NOT NULL,
        cost numeric NOT NULL,
        CONSTRAINT pk_prj_work_schedule_stage PRIMARY KEY (id),
        CONSTRAINT fk_prj_work_schedule_stage_cmn_document_prj_work_schedule_id FOREIGN KEY (prj_work_schedule_id) REFERENCES cmn_document (id) ON DELETE CASCADE
    );

    CREATE TABLE prj_work_schedule_sub_type (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        prj_work_schedule_id uuid NOT NULL,
        prj_work_schedule_stage_id uuid NOT NULL,
        work_sub_type_id uuid NOT NULL,
        measurement_unit character varying(50) NOT NULL,
        amount real NOT NULL,
        target real NOT NULL,
        begin_date timestamp without time zone NOT NULL,
        end_date timestamp without time zone NOT NULL,
        prj_project_work_schedule_stage_id uuid NULL,
        CONSTRAINT pk_prj_work_schedule_sub_type PRIMARY KEY (id),
        CONSTRAINT "fk_prj_work_schedule_sub_type_prj_work_schedule_stage_prj_proj~" FOREIGN KEY (prj_project_work_schedule_stage_id) REFERENCES prj_work_schedule_stage (id) ON DELETE RESTRICT,
        CONSTRAINT fk_prj_work_schedule_sub_type_cmn_document_prj_work_schedule_id FOREIGN KEY (prj_work_schedule_id) REFERENCES cmn_document (id) ON DELETE CASCADE,
        CONSTRAINT "fk_prj_work_schedule_sub_type_cdn_work_sub_type_work_sub_type_~" FOREIGN KEY (work_sub_type_id) REFERENCES cdn_work_sub_type (id) ON DELETE CASCADE
    );
    CREATE INDEX ix_prj_work_schedule_parent_id ON prj_work_schedule (parent_id);
    CREATE INDEX ix_prj_work_schedule_project_id ON prj_work_schedule (project_id);
    CREATE INDEX ix_prj_work_schedule_stage_prj_work_schedule_id ON prj_work_schedule_stage (prj_work_schedule_id);
    CREATE INDEX "ix_prj_work_schedule_sub_type_prj_project_work_schedule_stage_~" ON prj_work_schedule_sub_type (prj_project_work_schedule_stage_id);
    CREATE INDEX ix_prj_work_schedule_sub_type_prj_work_schedule_id ON prj_work_schedule_sub_type (prj_work_schedule_id);
    CREATE INDEX ix_prj_work_schedule_sub_type_work_sub_type_id ON prj_work_schedule_sub_type (work_sub_type_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201211160210_v004.30_ProjectWorkSchedule', '3.1.9');

    END IF;
END $$;