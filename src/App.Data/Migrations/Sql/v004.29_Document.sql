
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201210105602_v004.29_Document') THEN
    CREATE TABLE cmn_document (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        derived_entity text NULL,
        reg_number character varying(100) NOT NULL,
        reg_date timestamp without time zone NOT NULL,
        doc_type character varying(50) NOT NULL,
        doc_state character varying(50) NOT NULL,
        description character varying(500) NULL,
        parent_id uuid NULL,
        CONSTRAINT pk_cmn_document PRIMARY KEY (id)
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201210105602_v004.29_Document') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201210105602_v004.29_Document', '3.1.9');
    END IF;
END $$;
