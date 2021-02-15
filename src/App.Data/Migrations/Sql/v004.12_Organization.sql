
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201001211019_v004.12_Organization') THEN
    ALTER TABLE org_organization ADD org_state character varying(50) NOT NULL DEFAULT '';
    ALTER TABLE org_organization ADD organization_category character varying(50) NOT NULL DEFAULT '';

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201001211019_v004.12_Organization', '3.1.7');
    END IF;
END $$;