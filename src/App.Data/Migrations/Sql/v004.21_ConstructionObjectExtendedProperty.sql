
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201207081020_v004.21_ConstructionObjectExtendedProperty') THEN
    
    CREATE TABLE construction_object_extended_property (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        construction_object_id uuid NOT NULL,
        dictionary_id uuid NOT NULL,
        value text NOT NULL,
        value_json json NULL,
        CONSTRAINT pk_construction_object_extended_property PRIMARY KEY (id),
        CONSTRAINT "fk_construction_object_extended_property_construction_object_c~" FOREIGN KEY (construction_object_id) REFERENCES construction_object (id) ON DELETE CASCADE,
        CONSTRAINT "fk_construction_object_extended_property_construction_object_e~" FOREIGN KEY (dictionary_id) REFERENCES construction_object_ex_property_dictionary (id) ON DELETE CASCADE
    );

    CREATE INDEX ix_construction_object_extended_property_construction_object_id ON construction_object_extended_property (construction_object_id);
    CREATE INDEX ix_construction_object_extended_property_dictionary_id ON construction_object_extended_property (dictionary_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201207081020_v004.21_ConstructionObjectExtendedProperty', '3.1.9');

    END IF;
END $$;