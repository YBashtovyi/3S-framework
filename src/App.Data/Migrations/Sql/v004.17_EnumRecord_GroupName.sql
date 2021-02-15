
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119164605_v004.17_EnumRecord_GroupName') THEN
    
    ALTER TABLE cmn_enum_record ADD group_name character varying(50) NULL;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201119164605_v004.17_EnumRecord_GroupName', '3.1.9');

    END IF;
END $$;
