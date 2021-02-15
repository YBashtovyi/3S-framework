
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201216010710_v004.37_FileStore') THEN
        ALTER TABLE cmn_file_store DROP CONSTRAINT fk_cmn_file_store_cmn_enum_record_document_type_id;
        ALTER TABLE cmn_file_store DROP CONSTRAINT fk_cmn_file_store_cmn_enum_record_file_group_id;
        DROP INDEX ix_cmn_file_store_document_type_id;
        DROP INDEX ix_cmn_file_store_file_group_id;
        ALTER TABLE cmn_file_store DROP COLUMN document_type_id;
        ALTER TABLE cmn_file_store DROP COLUMN file_group_id;
        ALTER TABLE cmn_file_store ALTER COLUMN file_type TYPE character varying(5);
        ALTER TABLE cmn_file_store ALTER COLUMN file_type DROP NOT NULL;
        ALTER TABLE cmn_file_store ALTER COLUMN file_type DROP DEFAULT;
        ALTER TABLE cmn_file_store ALTER COLUMN file_path TYPE character varying(200);
        ALTER TABLE cmn_file_store ALTER COLUMN file_path DROP NOT NULL;
        ALTER TABLE cmn_file_store ALTER COLUMN file_path DROP DEFAULT;
        ALTER TABLE cmn_file_store ALTER COLUMN file_name TYPE character varying(100);
        ALTER TABLE cmn_file_store ALTER COLUMN file_name DROP NOT NULL;
        ALTER TABLE cmn_file_store ALTER COLUMN file_name DROP DEFAULT;
        ALTER TABLE cmn_file_store ALTER COLUMN entity_name TYPE character varying(50);
        ALTER TABLE cmn_file_store ALTER COLUMN entity_name DROP NOT NULL;
        ALTER TABLE cmn_file_store ALTER COLUMN entity_name DROP DEFAULT;
        ALTER TABLE cmn_file_store ALTER COLUMN description TYPE character varying(500);
        ALTER TABLE cmn_file_store ALTER COLUMN description DROP NOT NULL;
        ALTER TABLE cmn_file_store ALTER COLUMN description DROP DEFAULT;
        ALTER TABLE cmn_file_store ALTER COLUMN content_type TYPE character varying(50);
        ALTER TABLE cmn_file_store ALTER COLUMN content_type DROP NOT NULL;
        ALTER TABLE cmn_file_store ALTER COLUMN content_type DROP DEFAULT;
        ALTER TABLE cmn_file_store ADD owner_id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
        ALTER TABLE cmn_file_store ADD type_of_attached_file character varying(50) NOT NULL DEFAULT '';
        CREATE INDEX ix_cmn_file_store_owner_id ON cmn_file_store (owner_id);
        ALTER TABLE cmn_file_store ADD CONSTRAINT fk_cmn_file_store_org_unit_owner_id FOREIGN KEY (owner_id) REFERENCES org_unit (id) ON DELETE CASCADE;
        INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
        VALUES ('20201216010710_v004.37_FileStore', '3.1.9');
    END IF;
END $$;