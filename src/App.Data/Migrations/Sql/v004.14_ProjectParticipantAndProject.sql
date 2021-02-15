
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201007201924_v004.14_ProjectParticipantAndProject') THEN
    ALTER TABLE project DROP CONSTRAINT pk_project;
    ALTER TABLE project ALTER COLUMN const_obj_number TYPE character varying(100);
    ALTER TABLE project ALTER COLUMN const_obj_number DROP NOT NULL;
    ALTER TABLE project ALTER COLUMN const_obj_number DROP DEFAULT;
    ALTER TABLE project ADD CONSTRAINT pk_project PRIMARY KEY (id);

    CREATE TABLE prj_participant (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        project_id uuid NOT NULL,
        project_role character varying(50) NOT NULL,
        participant_id uuid NOT NULL,
        responsible_person_id uuid NOT NULL,
        description character varying(250) NULL,
        CONSTRAINT pk_prj_participant PRIMARY KEY (id),
        CONSTRAINT fk_prj_participant_organization_participant_id FOREIGN KEY (participant_id) REFERENCES organization (id) ON DELETE CASCADE,
        CONSTRAINT fk_prj_participant_project_project_id FOREIGN KEY (project_id) REFERENCES project (id) ON DELETE CASCADE,
        CONSTRAINT fk_prj_participant_org_employee_responsible_person_id FOREIGN KEY (responsible_person_id) REFERENCES org_employee (id) ON DELETE CASCADE
    );
    CREATE UNIQUE INDEX ix_project_owner_id_const_obj_number ON project (owner_id, const_obj_number);
    CREATE INDEX ix_prj_participant_participant_id ON prj_participant (participant_id);
    CREATE INDEX ix_prj_participant_project_id ON prj_participant (project_id);
    CREATE INDEX ix_prj_participant_responsible_person_id ON prj_participant (responsible_person_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201007201924_v004.14_ProjectParticipantAndProject', '3.1.7');

    END IF;
END $$;