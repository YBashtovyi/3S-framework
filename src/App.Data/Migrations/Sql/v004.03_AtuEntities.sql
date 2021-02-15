
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20200917155433_v004.03_AtuEntities') THEN
    ALTER TABLE atu_city DROP CONSTRAINT fk_atu_city_cmn_enum_record_type_id;
    DROP TABLE atu_city_district;
    DROP INDEX ix_atu_city_type_id;
    ALTER TABLE cdn_coordinate DROP CONSTRAINT pk_cdn_coordinate;
    ALTER TABLE atu_region DROP COLUMN caption;
    ALTER TABLE atu_region DROP COLUMN country_id;
    ALTER TABLE atu_region DROP COLUMN koatuu;
    ALTER TABLE atu_city DROP COLUMN caption;
    ALTER TABLE atu_city DROP COLUMN code;
    ALTER TABLE atu_city DROP COLUMN region_id;
    ALTER TABLE atu_city DROP COLUMN type_id;

    ALTER TABLE cdn_coordinate RENAME TO atu_coordinate;
    ALTER TABLE atu_street ADD atu_street_type character varying(50) NULL;
    ALTER TABLE atu_street ADD name character varying(200) NULL;

    ALTER TABLE atu_region ALTER COLUMN parent_id TYPE uuid;
    ALTER TABLE atu_region ALTER COLUMN parent_id SET NOT NULL;
    ALTER TABLE atu_region ALTER COLUMN parent_id DROP DEFAULT;
    ALTER TABLE atu_region ALTER COLUMN code TYPE character varying(100);
    ALTER TABLE atu_region ALTER COLUMN code DROP NOT NULL;
    ALTER TABLE atu_region ALTER COLUMN code DROP DEFAULT;
    ALTER TABLE atu_region ADD atu_region_type character varying(50) NULL;
    ALTER TABLE atu_region ADD koatu character varying(50) NULL;
    ALTER TABLE atu_region ADD name character varying(200) NULL;

    ALTER TABLE atu_city ADD atu_city_type character varying(50) NULL;
    ALTER TABLE atu_city ADD name character varying(200) NULL;
    ALTER TABLE atu_coordinate ADD CONSTRAINT pk_atu_coordinate PRIMARY KEY (id);

    CREATE TABLE atu_address (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        street_id uuid NOT NULL,
        zip_code character varying(50) NULL,
        building character varying(50) NULL,
        entrance character varying(50) NULL,
        apartment character varying(50) NULL,
        comment text NULL,
        CONSTRAINT pk_atu_address PRIMARY KEY (id),
        CONSTRAINT fk_atu_address_atu_street_street_id FOREIGN KEY (street_id) REFERENCES atu_street (id) ON DELETE CASCADE
    );

    CREATE TABLE atu_district (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        name text NULL,
        code character varying(100) NULL,
        parent_id uuid NOT NULL,
        atu_district_type character varying(50) NULL,
        CONSTRAINT pk_atu_district PRIMARY KEY (id)
    );

    CREATE TABLE atu_subject (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        derived_entity text NULL,
        name character varying(200) NULL,
        code character varying(100) NULL,
        parent_id uuid NOT NULL,
        CONSTRAINT pk_atu_subject PRIMARY KEY (id)
    );

    CREATE TABLE atu_city_district_streets_map (
        city_id uuid NOT NULL,
        district_id uuid NOT NULL,
        street_id uuid NOT NULL,
        CONSTRAINT pk_atu_city_district_streets_map PRIMARY KEY (city_id, district_id, street_id),
        CONSTRAINT fk_atu_city_district_streets_map_atu_city_city_id FOREIGN KEY (city_id) REFERENCES atu_city (id) ON DELETE CASCADE,
        CONSTRAINT fk_atu_city_district_streets_map_atu_district_district_id FOREIGN KEY (district_id) REFERENCES atu_district (id) ON DELETE CASCADE,
        CONSTRAINT fk_atu_city_district_streets_map_atu_street_street_id FOREIGN KEY (street_id) REFERENCES atu_street (id) ON DELETE CASCADE
    );

    CREATE INDEX ix_atu_address_street_id ON atu_address (street_id);
    CREATE INDEX ix_atu_city_district_streets_map_district_id ON atu_city_district_streets_map (district_id);
    CREATE INDEX ix_atu_city_district_streets_map_street_id ON atu_city_district_streets_map (street_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200917155433_v004.03_AtuEntities', '3.1.7');

    END IF;
END $$;
