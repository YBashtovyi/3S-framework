
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200928173654_v004.10_User') THEN
    ALTER TABLE adm_user DROP CONSTRAINT pk_adm_user;
    ALTER TABLE adm_user ADD id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    ALTER TABLE adm_user ADD created_by uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    ALTER TABLE adm_user ADD created_on timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';
    ALTER TABLE adm_user ADD modified_by uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    ALTER TABLE adm_user ADD modified_on timestamp without time zone NULL;
    ALTER TABLE adm_user ADD record_state integer NOT NULL DEFAULT 0;
    ALTER TABLE adm_user ADD CONSTRAINT pk_adm_user PRIMARY KEY (id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200928173654_v004.10_User', '3.1.7');

    END IF;
END $$;
