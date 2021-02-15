
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200918082826_v004.05_AtuRequired') THEN
    ALTER TABLE atu_subject ALTER COLUMN name TYPE character varying(200);
    ALTER TABLE atu_subject ALTER COLUMN name SET NOT NULL;
    ALTER TABLE atu_subject ALTER COLUMN name DROP DEFAULT;

    ALTER TABLE atu_subject ALTER COLUMN code TYPE character varying(100);
    ALTER TABLE atu_subject ALTER COLUMN code SET NOT NULL;
    ALTER TABLE atu_subject ALTER COLUMN code DROP DEFAULT;

    ALTER TABLE atu_street ALTER COLUMN name TYPE character varying(200);
    ALTER TABLE atu_street ALTER COLUMN name SET NOT NULL;
    ALTER TABLE atu_street ALTER COLUMN name DROP DEFAULT;

    ALTER TABLE atu_street ALTER COLUMN atu_street_type TYPE character varying(50);
    ALTER TABLE atu_street ALTER COLUMN atu_street_type SET NOT NULL;
    ALTER TABLE atu_street ALTER COLUMN atu_street_type DROP DEFAULT;

    ALTER TABLE atu_region ALTER COLUMN name TYPE character varying(200);
    ALTER TABLE atu_region ALTER COLUMN name SET NOT NULL;
    ALTER TABLE atu_region ALTER COLUMN name DROP DEFAULT;

    ALTER TABLE atu_region ALTER COLUMN koatu TYPE character varying(50);
    ALTER TABLE atu_region ALTER COLUMN koatu SET NOT NULL;
    ALTER TABLE atu_region ALTER COLUMN koatu DROP DEFAULT;

    ALTER TABLE atu_region ALTER COLUMN code TYPE character varying(100);
    ALTER TABLE atu_region ALTER COLUMN code SET NOT NULL;
    ALTER TABLE atu_region ALTER COLUMN code DROP DEFAULT;

    ALTER TABLE atu_region ALTER COLUMN atu_region_type TYPE character varying(50);
    ALTER TABLE atu_region ALTER COLUMN atu_region_type SET NOT NULL;
    ALTER TABLE atu_region ALTER COLUMN atu_region_type DROP DEFAULT;

    ALTER TABLE atu_district ALTER COLUMN name TYPE text;
    ALTER TABLE atu_district ALTER COLUMN name SET NOT NULL;
    ALTER TABLE atu_district ALTER COLUMN name DROP DEFAULT;

    ALTER TABLE atu_district ALTER COLUMN code TYPE character varying(100);
    ALTER TABLE atu_district ALTER COLUMN code SET NOT NULL;
    ALTER TABLE atu_district ALTER COLUMN code DROP DEFAULT;

    ALTER TABLE atu_district ALTER COLUMN atu_district_type TYPE character varying(50);
    ALTER TABLE atu_district ALTER COLUMN atu_district_type SET NOT NULL;
    ALTER TABLE atu_district ALTER COLUMN atu_district_type DROP DEFAULT;

    ALTER TABLE atu_city ALTER COLUMN name TYPE character varying(200);
    ALTER TABLE atu_city ALTER COLUMN name SET NOT NULL;
    ALTER TABLE atu_city ALTER COLUMN name DROP DEFAULT;

    ALTER TABLE atu_city ALTER COLUMN atu_city_type TYPE character varying(50);
    ALTER TABLE atu_city ALTER COLUMN atu_city_type SET NOT NULL;
    ALTER TABLE atu_city ALTER COLUMN atu_city_type DROP DEFAULT;

    ALTER TABLE atu_address ALTER COLUMN zip_code TYPE character varying(50);
    ALTER TABLE atu_address ALTER COLUMN zip_code SET NOT NULL;
    ALTER TABLE atu_address ALTER COLUMN zip_code DROP DEFAULT;

    ALTER TABLE atu_address ALTER COLUMN entrance TYPE character varying(50);
    ALTER TABLE atu_address ALTER COLUMN entrance SET NOT NULL;
    ALTER TABLE atu_address ALTER COLUMN entrance DROP DEFAULT;

    ALTER TABLE atu_address ALTER COLUMN building TYPE character varying(50);
    ALTER TABLE atu_address ALTER COLUMN building SET NOT NULL;
    ALTER TABLE atu_address ALTER COLUMN building DROP DEFAULT;

    ALTER TABLE atu_address ALTER COLUMN apartment TYPE character varying(50);
    ALTER TABLE atu_address ALTER COLUMN apartment SET NOT NULL;
    ALTER TABLE atu_address ALTER COLUMN apartment DROP DEFAULT;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200918082826_v004.05_AtuRequired', '3.1.7');

    END IF;
END $$;