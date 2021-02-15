
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200917185852_v004.04_Project') THEN
    CREATE TABLE project (
        owner_id uuid NOT NULL,
        const_obj_number character varying(100) NOT NULL,
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        name character varying(100) NOT NULL,
        region_id uuid NOT NULL,
        district_id uuid NOT NULL,
        code character varying(20) NOT NULL,
        type_of_construction_object character varying(50) NOT NULL,
        project_status character varying(50) NOT NULL,
        type_of_financing character varying(50) NOT NULL,
        cost numeric NOT NULL,
        implementation_state character varying(50) NOT NULL,
        date_begin timestamp without time zone NOT NULL,
        date_end timestamp without time zone NOT NULL,
        CONSTRAINT pk_project PRIMARY KEY (owner_id, const_obj_number),
        CONSTRAINT fk_project_atu_district_district_id FOREIGN KEY (district_id) REFERENCES atu_district (id) ON DELETE CASCADE,
        CONSTRAINT fk_project_org_unit_owner_id FOREIGN KEY (owner_id) REFERENCES org_unit (id) ON DELETE CASCADE,
        CONSTRAINT fk_project_atu_region_region_id FOREIGN KEY (region_id) REFERENCES atu_region (id) ON DELETE CASCADE
    );

    CREATE UNIQUE INDEX ix_project_code ON project (code);
    CREATE INDEX ix_project_district_id ON project (district_id);
    CREATE INDEX ix_project_region_id ON project (region_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200917185852_v004.04_Project', '3.1.7');
    END IF;
END $$;