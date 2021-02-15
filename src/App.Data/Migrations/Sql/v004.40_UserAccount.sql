
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210115123742_v004.40_UserAccount') THEN
    ALTER TABLE adm_user DROP CONSTRAINT pk_adm_user;
    ALTER TABLE adm_user ADD CONSTRAINT pk_adm_user PRIMARY KEY (id);
    ALTER TABLE adm_user DROP COLUMN account_id;
    CREATE TABLE adm_user_account (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        user_id uuid NOT NULL,
        auth_provider character varying(50) NULL,
        account_id character varying(36) NULL,
        CONSTRAINT pk_adm_user_account PRIMARY KEY (id),
        CONSTRAINT fk_adm_user_account_adm_user_user_id FOREIGN KEY (user_id) REFERENCES adm_user (id) ON DELETE CASCADE
    );
    CREATE INDEX ix_adm_user_account_user_id ON adm_user_account (user_id);
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20210115123742_v004.40_UserAccount', '3.1.9');

    END IF;
END $$;