
DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201006172436_v004.13_OrgUnit') THEN
    ALTER TABLE cmn_notification DROP CONSTRAINT fk_cmn_notification_organization_organization_id;
    ALTER TABLE cmn_notification_receiver DROP CONSTRAINT fk_cmn_notification_receiver_organization_organization_id;
    ALTER TABLE eq_schedule_resource DROP CONSTRAINT fk_eq_schedule_resource_organization_organization_id;
    ALTER TABLE eq_schedule_setting DROP CONSTRAINT fk_eq_schedule_setting_organization_organization_id;
    ALTER TABLE eq_schedule_setting_property DROP CONSTRAINT "fk_eq_schedule_setting_property_organization_organization_~";
    ALTER TABLE eq_schedule_slot DROP CONSTRAINT fk_eq_schedule_slot_organization_organization_id;
    ALTER TABLE eq_schedule_time DROP CONSTRAINT fk_eq_schedule_time_organization_organization_id;
    ALTER TABLE org_department DROP CONSTRAINT fk_org_department_organization_organization_id;
    ALTER TABLE organization DROP CONSTRAINT pk_organization;

    ALTER TABLE org_unit DROP COLUMN org_unit_type;

    ALTER TABLE organization RENAME TO organization;
    ALTER INDEX ix_organization_code RENAME TO ix_organization_code;
    ALTER TABLE organization ADD CONSTRAINT pk_organization PRIMARY KEY (id);
    ALTER TABLE cmn_notification ADD CONSTRAINT fk_cmn_notification_organization_organization_id FOREIGN KEY (organization_id) REFERENCES organization (id) ON DELETE RESTRICT;
    ALTER TABLE cmn_notification_receiver ADD CONSTRAINT fk_cmn_notification_receiver_organization_organization_id FOREIGN KEY (organization_id) REFERENCES organization (id) ON DELETE RESTRICT;
    ALTER TABLE eq_schedule_resource ADD CONSTRAINT fk_eq_schedule_resource_organization_organization_id FOREIGN KEY (organization_id) REFERENCES organization (id) ON DELETE CASCADE;
    ALTER TABLE eq_schedule_setting ADD CONSTRAINT fk_eq_schedule_setting_organization_organization_id FOREIGN KEY (organization_id) REFERENCES organization (id) ON DELETE CASCADE;
    ALTER TABLE eq_schedule_setting_property ADD CONSTRAINT fk_eq_schedule_setting_property_organization_organization_id FOREIGN KEY (organization_id) REFERENCES organization (id) ON DELETE CASCADE;
    ALTER TABLE eq_schedule_slot ADD CONSTRAINT fk_eq_schedule_slot_organization_organization_id FOREIGN KEY (organization_id) REFERENCES organization (id) ON DELETE CASCADE;
    ALTER TABLE eq_schedule_time ADD CONSTRAINT fk_eq_schedule_time_organization_organization_id FOREIGN KEY (organization_id) REFERENCES organization (id) ON DELETE CASCADE;
    ALTER TABLE org_department ADD CONSTRAINT fk_org_department_organization_organization_id FOREIGN KEY (organization_id) REFERENCES organization (id) ON DELETE CASCADE;
    
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201006172436_v004.13_OrgUnit', '3.1.7');

    END IF;
END $$;