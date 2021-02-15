
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201202231703_v004.19_ProjectConstructionObject') THEN
    ALTER TABLE project DROP COLUMN type_of_construction_object;

     CREATE TABLE prj_construction_object (
        id uuid NOT NULL,
        record_state integer NOT NULL,
        modified_by uuid NOT NULL,
        modified_on timestamp without time zone NULL,
        created_by uuid NOT NULL,
        created_on timestamp without time zone NOT NULL,
        project_id uuid NOT NULL,
        construction_object_id uuid NOT NULL,
        CONSTRAINT pk_prj_construction_object PRIMARY KEY (id),
        CONSTRAINT "fk_prj_construction_object_construction_object_construction_ob~" FOREIGN KEY (construction_object_id) REFERENCES construction_object (id) ON DELETE CASCADE,
        CONSTRAINT fk_prj_construction_object_project_project_id FOREIGN KEY (project_id) REFERENCES project (id) ON DELETE CASCADE
    );

    CREATE INDEX ix_prj_construction_object_construction_object_id ON prj_construction_object (construction_object_id);
    CREATE INDEX ix_prj_construction_object_project_id ON prj_construction_object (project_id);

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201202231703_v004.19_ProjectConstructionObject', '3.1.9');

    END IF;
END $$;