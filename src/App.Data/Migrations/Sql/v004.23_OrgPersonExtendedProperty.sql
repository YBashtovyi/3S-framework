
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201208191106_v004.23_OrgPersonExtendedProperty') THEN

    ALTER TABLE org_unit_extended_property DROP COLUMN property_type;
    ALTER TABLE cmn_person_extended_property DROP COLUMN person_property;

    ALTER TABLE org_unit_extended_property ALTER COLUMN value TYPE character varying(200);
    ALTER TABLE org_unit_extended_property ALTER COLUMN value DROP NOT NULL;
    ALTER TABLE org_unit_extended_property ALTER COLUMN value DROP DEFAULT;
    ALTER TABLE org_unit_extended_property ADD org_extended_property character varying(50) NULL;

    ALTER TABLE cmn_person_extended_property ALTER COLUMN value TYPE character varying(200);
    ALTER TABLE cmn_person_extended_property ALTER COLUMN value DROP NOT NULL;
    ALTER TABLE cmn_person_extended_property ALTER COLUMN value DROP DEFAULT;

    ALTER TABLE cmn_person_extended_property ADD person_extended_property character varying(50) NULL;
    ALTER TABLE cmn_person_extended_property ADD value_json json NULL;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201208191106_v004.23_OrgPersonExtendedProperty', '3.1.9');

    END IF;
END $$;