
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201215162436_v004.35_ProjectContract') THEN
    ALTER TABLE prj_contract ADD general_contractor_id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    ALTER TABLE prj_contract ADD customer_id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    CREATE INDEX ix_prj_contract_customer_id ON prj_contract (customer_id);
    CREATE INDEX ix_prj_contract_general_contractor_id ON prj_contract (general_contractor_id);
    ALTER TABLE prj_contract ADD CONSTRAINT fk_prj_contract_organization_customer_id FOREIGN KEY (customer_id) REFERENCES organization (id) ON DELETE CASCADE;
    ALTER TABLE prj_contract ADD CONSTRAINT fk_prj_contract_organization_general_contractor_id FOREIGN KEY (general_contractor_id) REFERENCES organization (id) ON DELETE CASCADE;
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201215162436_v004.35_ProjectContract', '3.1.9');
    END IF;
END $$;