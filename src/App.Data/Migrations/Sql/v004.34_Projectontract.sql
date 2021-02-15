
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201214225731_v004.34_Projectontract') THEN

    CREATE TABLE prj_contract (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        project_id uuid NOT NULL,
        doc_type character varying(50) NOT NULL,
        reg_date timestamp without time zone NOT NULL,
        reg_number character varying(100) NOT NULL,
        doc_state character varying(50) NOT NULL,
        cost numeric NOT NULL,
        bidding_type character varying(50) NOT NULL,
        tender_code character varying(100) NULL,
        bidding_code uuid NULL,
        bidding_subject character varying(2000) NULL,
        description character varying(500) NULL,
        CONSTRAINT pk_prj_contract PRIMARY KEY (id),
        CONSTRAINT fk_prj_contract_project_project_id FOREIGN KEY (project_id) REFERENCES project (id) ON DELETE CASCADE
    );
    CREATE INDEX ix_prj_contract_project_id ON prj_contract (project_id);
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201214225731_v004.34_Projectontract', '3.1.9');

    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201214225731_v004.34_Projectontract') THEN
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201214225731_v004.34_Projectontract') THEN
    
    END IF;
END $$;
