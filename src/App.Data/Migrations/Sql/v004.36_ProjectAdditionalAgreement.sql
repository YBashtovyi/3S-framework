
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201215214603_v004.36_ProjectAdditionalAgreement') THEN

    ALTER TABLE prj_work_schedule_sub_type DROP CONSTRAINT "fk_prj_work_schedule_sub_type_cdn_work_sub_type_work_sub_type_~";
    
    CREATE TABLE prj_additional_agreement (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        project_id uuid NOT NULL,
        parent_id uuid NOT NULL,
        doc_type character varying(50) NOT NULL,
        reg_date timestamp without time zone NOT NULL,
        reg_number character varying(100) NOT NULL,
        doc_state character varying(50) NOT NULL,
        cost numeric NOT NULL,
        description character varying(500) NULL,
        CONSTRAINT pk_prj_additional_agreement PRIMARY KEY (id),
        CONSTRAINT fk_prj_additional_agreement_prj_contract_parent_id FOREIGN KEY (parent_id) REFERENCES prj_contract (id) ON DELETE CASCADE,
        CONSTRAINT fk_prj_additional_agreement_project_project_id FOREIGN KEY (project_id) REFERENCES project (id) ON DELETE CASCADE
    );
    CREATE INDEX ix_prj_additional_agreement_parent_id ON prj_additional_agreement (parent_id);
    CREATE INDEX ix_prj_additional_agreement_project_id ON prj_additional_agreement (project_id);
    ALTER TABLE prj_work_schedule_sub_type ADD CONSTRAINT fk_prj_work_schedule_sub_type_cdn_work_sub_type_work_sub_type_id FOREIGN KEY (work_sub_type_id) REFERENCES cdn_work_sub_type (id) ON DELETE CASCADE;
    
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201215214603_v004.36_ProjectAdditionalAgreement', '3.1.9');

    END IF;
END $$;