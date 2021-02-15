
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201020124826_v004.15_Administration') THEN

    DROP TABLE sys_application_row_level_right;
    ALTER TABLE sys_user_default_value DROP CONSTRAINT ak_sys_user_default_value_user_id_entity_name;
    ALTER TABLE sys_row_level_right DROP CONSTRAINT ak_sys_row_level_right_profile_id_entity_name_main_entity_name;
    ALTER TABLE adm_user DROP COLUMN adm_user_state;
    ALTER TABLE adm_user DROP COLUMN identity_id;
    ALTER TABLE org_employee ADD discriminator text NOT NULL DEFAULT '';
    ALTER TABLE adm_user ADD account_id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    ALTER TABLE adm_user ADD login character varying(100) NULL;
    ALTER TABLE adm_user ADD rls json NULL;
    ALTER TABLE adm_user ADD roles json NULL;
    CREATE TABLE adm_right (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        right_type character varying(50) NOT NULL,
        name character varying(100) NOT NULL,
        code character varying(50) NOT NULL,
        description character varying(250) NOT NULL,
        els json NULL,
        CONSTRAINT pk_adm_right PRIMARY KEY (id)
    );

    CREATE TABLE adm_role (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        name character varying(100) NOT NULL,
        code character varying(50) NOT NULL,
        is_active boolean NOT NULL,
        description character varying(250) NOT NULL,
        rls json NULL,
        CONSTRAINT pk_adm_role PRIMARY KEY (id)
    );

    CREATE TABLE adm_role_right (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        role_id uuid NOT NULL,
        right_id uuid NOT NULL,
        CONSTRAINT pk_adm_role_right PRIMARY KEY (id),
        CONSTRAINT fk_adm_role_right_adm_right_right_id FOREIGN KEY (right_id) REFERENCES adm_right (id) ON DELETE CASCADE,
        CONSTRAINT fk_adm_role_right_adm_role_role_id FOREIGN KEY (role_id) REFERENCES adm_role (id) ON DELETE CASCADE
    );

    CREATE INDEX ix_sys_user_default_value_user_id ON sys_user_default_value (user_id);
    CREATE INDEX ix_sys_row_level_right_profile_id ON sys_row_level_right (profile_id);
    CREATE INDEX ix_adm_role_right_right_id ON adm_role_right (right_id);
    CREATE INDEX ix_adm_role_right_role_id ON adm_role_right (role_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201020124826_v004.15_Administration', '3.1.9');

    END IF;
END $$;