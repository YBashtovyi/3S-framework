
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200915135736_v004.00_Initial') THEN
    ALTER TABLE cmn_enum_record DROP CONSTRAINT fk_cmn_enum_record_cmn_enum_record_parent_id;
    ALTER TABLE org_unit DROP CONSTRAINT fk_org_unit_cmn_enum_record_state_id;
    DROP INDEX ix_org_unit_state_id;
    DROP INDEX ix_cmn_enum_record_parent_id;
    ALTER TABLE org_unit DROP COLUMN state_id;
    ALTER TABLE cmn_enum_record DROP COLUMN caption;
    ALTER TABLE cmn_enum_record DROP COLUMN enum_type;
    ALTER TABLE cmn_enum_record DROP COLUMN parent_id;
    ALTER TABLE cmn_enum_record DROP COLUMN value_type;
    ALTER TABLE org_unit ADD org_unit_type text NULL;

    ALTER TABLE cmn_enum_record ALTER COLUMN code TYPE character varying(50);
    ALTER TABLE cmn_enum_record ALTER COLUMN code DROP NOT NULL;
    ALTER TABLE cmn_enum_record ALTER COLUMN code DROP DEFAULT;

    ALTER TABLE cmn_enum_record ADD "group" character varying(50) NULL;
    ALTER TABLE cmn_enum_record ADD item_number smallint NOT NULL DEFAULT 0;
    ALTER TABLE cmn_enum_record ADD name character varying(100) NULL;
    ALTER TABLE cmn_enum_record ADD value text NULL;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200915135736_v004.00_Initial', '3.1.7');
    END IF;
END $$;