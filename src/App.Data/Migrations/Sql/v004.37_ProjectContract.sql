
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201216140630_v004.37_ProjectContract') THEN
    ALTER TABLE prj_contract ALTER COLUMN bidding_subject TYPE character varying(2000);
    ALTER TABLE prj_contract ALTER COLUMN bidding_subject SET NOT NULL;
    ALTER TABLE prj_contract ALTER COLUMN bidding_subject DROP DEFAULT;

    ALTER TABLE prj_additional_agreement ALTER COLUMN cost TYPE numeric;
    ALTER TABLE prj_additional_agreement ALTER COLUMN cost DROP NOT NULL;
    ALTER TABLE prj_additional_agreement ALTER COLUMN cost DROP DEFAULT;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201216140630_v004.37_ProjectContract', '3.1.9');
    END IF;
END $$;
