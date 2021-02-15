
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201103233254_v004.16_OrgUnitStaff_MainPosition') THEN
    
    ALTER TABLE org_unit_staff ADD is_main_position boolean NOT NULL DEFAULT FALSE;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201103233254_v004.16_OrgUnitStaff_MainPosition', '3.1.9');
    
    END IF;
END $$;