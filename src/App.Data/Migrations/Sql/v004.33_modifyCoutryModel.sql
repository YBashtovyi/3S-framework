
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201213220134_v004.33_modifyCoutryModel') THEN
    ALTER TABLE atu_country ALTER COLUMN code TYPE character varying(10);
    ALTER TABLE atu_country ALTER COLUMN code SET NOT NULL;
    ALTER TABLE atu_country ALTER COLUMN code DROP DEFAULT;


    ALTER TABLE atu_country ALTER COLUMN caption TYPE character varying(200);
    ALTER TABLE atu_country ALTER COLUMN caption DROP NOT NULL;
    ALTER TABLE atu_country ALTER COLUMN caption DROP DEFAULT;

    ALTER TABLE atu_country ADD comment character varying(200) NULL;
    ALTER TABLE atu_country ADD full_name character varying(200) NOT NULL DEFAULT '';
    ALTER TABLE atu_country ADD name character varying(100) NOT NULL DEFAULT '';

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201213220134_v004.33_modifyCoutryModel', '3.1.9');

    END IF;
END $$;



