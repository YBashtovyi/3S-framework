
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201216174750_v004.38_ProjectPhotoReport') THEN
    CREATE TABLE prj_photo_report (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        project_id uuid NOT NULL,
        customer_id uuid NOT NULL,
        general_contractor_id uuid NOT NULL,
        doc_type character varying(50) NOT NULL,
        reg_date timestamp without time zone NOT NULL,
        reg_number character varying(100) NOT NULL,
        doc_state character varying(50) NOT NULL,
        fixation_type character varying(50) NOT NULL,
        fixation_state character varying(50) NOT NULL,
        responsible_employee_id uuid NULL,
        description character varying(1000) NULL,
        CONSTRAINT pk_prj_photo_report PRIMARY KEY (id),
        CONSTRAINT fk_prj_photo_report_organization_customer_id FOREIGN KEY (customer_id) REFERENCES organization (id) ON DELETE CASCADE,
        CONSTRAINT fk_prj_photo_report_organization_general_contractor_id FOREIGN KEY (general_contractor_id) REFERENCES organization (id) ON DELETE CASCADE,
        CONSTRAINT fk_prj_photo_report_project_project_id FOREIGN KEY (project_id) REFERENCES project (id) ON DELETE CASCADE,
        CONSTRAINT fk_prj_photo_report_org_employee_responsible_employee_id FOREIGN KEY (responsible_employee_id) REFERENCES org_employee (id) ON DELETE RESTRICT
    );
    CREATE INDEX ix_prj_photo_report_customer_id ON prj_photo_report (customer_id);
    CREATE INDEX ix_prj_photo_report_general_contractor_id ON prj_photo_report (general_contractor_id);
    CREATE INDEX ix_prj_photo_report_project_id ON prj_photo_report (project_id);
    CREATE INDEX ix_prj_photo_report_responsible_employee_id ON prj_photo_report (responsible_employee_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201216174750_v004.38_ProjectPhotoReport', '3.1.9');

    END IF;
END $$;