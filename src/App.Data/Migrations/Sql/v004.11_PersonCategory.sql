
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201001200249_v004.11_PersonCategory') THEN
    DROP TABLE cmn_person_population_category;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201001200249_v004.11_PersonCategory', '3.1.7');
    END IF;
END $$;
