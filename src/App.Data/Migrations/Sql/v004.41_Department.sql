
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210210171217_v004.41_Department') THEN
    ALTER TABLE org_department DROP COLUMN caption;

    ALTER TABLE org_department ALTER COLUMN name TYPE character varying(200);
    ALTER TABLE org_department ALTER COLUMN name DROP NOT NULL;
    ALTER TABLE org_department ALTER COLUMN name DROP DEFAULT;

    ALTER TABLE org_department ALTER COLUMN full_name TYPE character varying(200);
    ALTER TABLE org_department ALTER COLUMN full_name DROP NOT NULL;
    ALTER TABLE org_department ALTER COLUMN full_name DROP DEFAULT;

    ALTER TABLE org_department ALTER COLUMN description TYPE character varying(500);
    ALTER TABLE org_department ALTER COLUMN description DROP NOT NULL;
    ALTER TABLE org_department ALTER COLUMN description DROP DEFAULT;

    ALTER TABLE org_department ALTER COLUMN department_type TYPE character varying(50);
    ALTER TABLE org_department ALTER COLUMN department_type DROP NOT NULL;
    ALTER TABLE org_department ALTER COLUMN department_type DROP DEFAULT;

    ALTER TABLE org_department ALTER COLUMN code TYPE character varying(50);
    ALTER TABLE org_department ALTER COLUMN code DROP NOT NULL;
    ALTER TABLE org_department ALTER COLUMN code DROP DEFAULT;
    ALTER TABLE org_department ADD department_state character varying(50) NULL;

    INSERT INTO cmn_enum_record VALUES ('ae0a25c9-dd92-4e4b-999f-782d49ae1aa2', 2, '00000000-0000-0000-0000-000000000000', NULL, 'e7a4e10e-9836-4bb1-8b5a-3ede4bf314a3', '2020-10-08 18:34:48.570238', 'Disband', 'DepartmentState', 0, 'Розформовано', NULL, 'Стани підрозділів');
    INSERT INTO cmn_enum_record VALUES ('5673863c-ed45-407e-b855-f7cb2d24fa26', 2, '00000000-0000-0000-0000-000000000000', NULL, 'e7a4e10e-9836-4bb1-8b5a-3ede4bf314a3', '2020-10-08 18:34:48.570238', 'Active', 'DepartmentState', 0, 'Діє', NULL, 'Стани підрозділів');
    INSERT INTO cmn_enum_record VALUES ('61e912c7-7c0a-45c1-8eeb-bdb1e5d1e998', 2, '00000000-0000-0000-0000-000000000000', NULL, 'e7a4e10e-9836-4bb1-8b5a-3ede4bf314a3', '2020-10-08 18:34:48.570238', 'Project', 'DepartmentState', 0, 'Проєкт', NULL, 'Стани підрозділів');

    UPDATE org_department SET department_state = 'ACTIVE' WHERE department_state is null;

    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20210210171217_v004.41_Department', '3.1.9');
    END IF;
END $$;