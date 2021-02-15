
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201202104631_v004.18_ConstructionObject') THEN

    CREATE TABLE construction_object (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        code character varying(20) NOT NULL,
        object_status character varying(50) NOT NULL,
        name character varying(100) NOT NULL,
        full_name character varying(100) NOT NULL,
        type_of_construction_object character varying(50) NOT NULL,
        class_of_consequence character varying(50) NOT NULL,
        atu_coordinates json NULL,
        description character varying(250) NULL,
        CONSTRAINT pk_construction_object PRIMARY KEY (id)
    );

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201202104631_v004.18_ConstructionObject', '3.1.9');

    END IF;
END $$;
