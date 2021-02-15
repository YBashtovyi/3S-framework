
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200928081355_v004.08_Org') THEN
    ALTER TABLE cmn_person DROP CONSTRAINT fk_cmn_person_cmn_enum_record_gender_id;
    ALTER TABLE cmn_person DROP CONSTRAINT fk_cmn_person_org_organization_owner_id;
    ALTER TABLE org_employee DROP CONSTRAINT fk_org_employee_org_department_department_id;
    ALTER TABLE org_employee DROP CONSTRAINT fk_org_employee_org_organization_organization_id;
    DROP INDEX ix_org_employee_department_id;
    DROP INDEX ix_org_employee_organization_id;
    DROP INDEX ix_cmn_person_gender_id;
    DROP INDEX ix_cmn_person_owner_id;
    ALTER TABLE org_organization DROP COLUMN base_entity;
    ALTER TABLE org_organization DROP COLUMN category;
    ALTER TABLE org_employee DROP COLUMN caption;
    ALTER TABLE org_employee DROP COLUMN department_id;
    ALTER TABLE org_employee DROP COLUMN organization_id;
    ALTER TABLE cmn_person DROP COLUMN description;
    ALTER TABLE cmn_person DROP COLUMN email;
    ALTER TABLE cmn_person DROP COLUMN gender_id;
    ALTER TABLE cmn_person DROP COLUMN ipn;
    ALTER TABLE cmn_person DROP COLUMN location;
    ALTER TABLE cmn_person DROP COLUMN name;
    ALTER TABLE cmn_person DROP COLUMN no_ipn;
    ALTER TABLE cmn_person DROP COLUMN owner_id;
    ALTER TABLE cmn_person DROP COLUMN phone;
    ALTER TABLE cmn_person DROP COLUMN photo;
    ALTER TABLE cmn_person DROP COLUMN subscribed_to_notifications;
    ALTER TABLE cmn_person DROP COLUMN user_id;

    ALTER TABLE org_organization ALTER COLUMN full_name TYPE character varying(200);
    ALTER TABLE org_organization ALTER COLUMN full_name DROP NOT NULL;
    ALTER TABLE org_organization ALTER COLUMN full_name DROP DEFAULT;
    ALTER TABLE org_organization ALTER COLUMN description TYPE character varying(500);
    ALTER TABLE org_organization ALTER COLUMN description DROP NOT NULL;
    ALTER TABLE org_organization ALTER COLUMN description DROP DEFAULT;
    ALTER TABLE org_organization ALTER COLUMN code TYPE character varying(50);
    ALTER TABLE org_organization ALTER COLUMN code DROP NOT NULL;
    ALTER TABLE org_organization ALTER COLUMN code DROP DEFAULT;
    ALTER TABLE org_organization ADD edrpou character varying(50) NULL;
    ALTER TABLE org_organization ADD name character varying(200) NULL;
    ALTER TABLE org_organization ADD org_type character varying(50) NULL;

    ALTER TABLE cmn_person ALTER COLUMN middle_name TYPE character varying(100);
    ALTER TABLE cmn_person ALTER COLUMN middle_name SET NOT NULL;
    ALTER TABLE cmn_person ALTER COLUMN middle_name DROP DEFAULT;
    ALTER TABLE cmn_person ALTER COLUMN last_name TYPE character varying(100);
    ALTER TABLE cmn_person ALTER COLUMN last_name SET NOT NULL;
    ALTER TABLE cmn_person ALTER COLUMN last_name DROP DEFAULT;
    ALTER TABLE cmn_person ADD first_name character varying(100) NOT NULL DEFAULT '';
    ALTER TABLE cmn_person ADD gender character varying(50) NULL;
    ALTER TABLE cmn_person ADD identity_document character varying(50) NULL;
    ALTER TABLE cmn_person ADD no_tax_number boolean NOT NULL DEFAULT FALSE;
    ALTER TABLE cmn_person ADD tax_number character varying(20) NULL;

    CREATE TABLE cdn_position (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        code character varying(50) NULL,
        name character varying(100) NULL,
        caption character varying(100) NULL,
        CONSTRAINT pk_cdn_position PRIMARY KEY (id)
    );

    CREATE TABLE cmn_person_extended_property (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        person_id uuid NOT NULL,
        person_property character varying(50) NULL,
        value json NULL,
        CONSTRAINT pk_cmn_person_extended_property PRIMARY KEY (id)
    );

    CREATE TABLE org_unit_atu_address (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        org_unit_id uuid NULL,
        atu_address_id uuid NULL,
        address_type character varying(50) NULL,
        comment character varying(250) NULL,
        CONSTRAINT pk_org_unit_atu_address PRIMARY KEY (id),
        CONSTRAINT fk_org_unit_atu_address_atu_address_atu_address_id FOREIGN KEY (atu_address_id) REFERENCES atu_address (id) ON DELETE RESTRICT,
        CONSTRAINT fk_org_unit_atu_address_org_unit_org_unit_id FOREIGN KEY (org_unit_id) REFERENCES org_unit (id) ON DELETE RESTRICT
    );

    CREATE TABLE org_unit_extended_property (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        org_unit_id uuid NOT NULL,
        property_type character varying(50) NULL,
        value character varying(100) NULL,
        value_json json NULL,
        CONSTRAINT pk_org_unit_extended_property PRIMARY KEY (id),
        CONSTRAINT fk_org_unit_extended_property_org_unit_org_unit_id FOREIGN KEY (org_unit_id) REFERENCES org_unit (id) ON DELETE CASCADE
    );

    CREATE TABLE org_unit_position (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        org_unit_id uuid NOT NULL,
        position_id uuid NOT NULL,
        staff_unit_count numeric NOT NULL,
        description character varying(250) NULL,
        CONSTRAINT pk_org_unit_position PRIMARY KEY (id),
        CONSTRAINT fk_org_unit_position_org_unit_org_unit_id FOREIGN KEY (org_unit_id) REFERENCES org_unit (id) ON DELETE CASCADE,
        CONSTRAINT fk_org_unit_position_cdn_position_position_id FOREIGN KEY (position_id) REFERENCES cdn_position (id) ON DELETE CASCADE
    );

    CREATE TABLE org_unit_staff (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        org_unit_position_id uuid NOT NULL,
        employee_id uuid NOT NULL,
        start_date timestamp without time zone NOT NULL,
        end_date timestamp without time zone NOT NULL,
        CONSTRAINT pk_org_unit_staff PRIMARY KEY (id),
        CONSTRAINT fk_org_unit_staff_org_employee_employee_id FOREIGN KEY (employee_id) REFERENCES org_employee (id) ON DELETE CASCADE,
        CONSTRAINT fk_org_unit_staff_org_unit_position_org_unit_position_id FOREIGN KEY (org_unit_position_id) REFERENCES org_unit_position (id) ON DELETE CASCADE
    );

    CREATE UNIQUE INDEX ix_org_organization_code ON org_organization (code);
    CREATE INDEX ix_org_unit_atu_address_atu_address_id ON org_unit_atu_address (atu_address_id);
    CREATE INDEX ix_org_unit_atu_address_org_unit_id ON org_unit_atu_address (org_unit_id);
    CREATE INDEX ix_org_unit_extended_property_org_unit_id ON org_unit_extended_property (org_unit_id);
    CREATE INDEX ix_org_unit_position_org_unit_id ON org_unit_position (org_unit_id);
    CREATE INDEX ix_org_unit_position_position_id ON org_unit_position (position_id);
    CREATE INDEX ix_org_unit_staff_employee_id ON org_unit_staff (employee_id);
    CREATE INDEX ix_org_unit_staff_org_unit_position_id ON org_unit_staff (org_unit_position_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200928081355_v004.08_Org', '3.1.7');

    END IF;
END $$;
