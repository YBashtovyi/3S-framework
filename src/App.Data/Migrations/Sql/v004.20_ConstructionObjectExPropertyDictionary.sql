
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201204191949_v004.20_ConstructionObjectExPropertyDictionary') THEN
    
    CREATE TABLE construction_object_ex_property_dictionary (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        code character varying(32) NOT NULL,
        name character varying(200) NOT NULL,
        full_name character varying(300) NOT NULL,
        data_format character varying(50) NOT NULL,
        parent_id uuid NULL,
        CONSTRAINT pk_construction_object_ex_property_dictionary PRIMARY KEY (id)
    );

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201204191949_v004.20_ConstructionObjectExPropertyDictionary', '3.1.9');

    END IF;
END $$;
