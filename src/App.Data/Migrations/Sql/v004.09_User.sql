
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200928164017_v004.09_User') THEN
    CREATE TABLE adm_user (
        employee_id uuid NOT NULL,
        identity_id uuid NOT NULL,
        adm_user_state character varying(50) NOT NULL,
        CONSTRAINT pk_adm_user PRIMARY KEY (identity_id, employee_id),
        CONSTRAINT fk_adm_user_org_employee_employee_id FOREIGN KEY (employee_id) REFERENCES org_employee (id) ON DELETE CASCADE
    );

    CREATE INDEX ix_adm_user_employee_id ON adm_user (employee_id);
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200928164017_v004.09_User', '3.1.7');

    END IF;
END $$;
