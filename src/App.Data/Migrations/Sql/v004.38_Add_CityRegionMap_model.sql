DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201217072216_v004.38_Add_CityRegionMap_model') THEN
    CREATE TABLE atu_city_region_map (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        city_id uuid NOT NULL,
        region_id uuid NOT NULL,
        CONSTRAINT pk_atu_city_region_map PRIMARY KEY (id),
        CONSTRAINT fk_atu_city_region_map_atu_city_city_id FOREIGN KEY (city_id) REFERENCES atu_city (id) ON DELETE CASCADE,
        CONSTRAINT fk_atu_city_region_map_atu_region_region_id FOREIGN KEY (region_id) REFERENCES atu_region (id) ON DELETE CASCADE
    );
    END IF;

    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201217072216_v004.38_Add_CityRegionMap_model') THEN
    CREATE INDEX ix_atu_city_region_map_city_id ON atu_city_region_map (city_id);
    END IF;

    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201217072216_v004.38_Add_CityRegionMap_model') THEN
    CREATE INDEX ix_atu_city_region_map_region_id ON atu_city_region_map (region_id);
    END IF;

    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201217072216_v004.38_Add_CityRegionMap_model') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201217072216_v004.38_Add_CityRegionMap_model', '3.1.9');
    END IF;

END $$;
