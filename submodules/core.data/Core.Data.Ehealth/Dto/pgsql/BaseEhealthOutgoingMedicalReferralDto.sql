﻿select x.id,
    coalesce(x.caption, '') as caption,
    x.organization_id,
    x.employee_id,
    x.ehealth_employee_id,
    x.patient_card_id,
    x.status_id,
    x.processing_status_in_ehealth_id,
    x.medical_service_program_id,
    x.reg_date,
    x.expiration_date,
    x.priority_id,
    x.medical_referral_category_id,
    x.performer_type_id,
    x.service_catalog_service_id,
    x.service_catalog_group_id,
    x.performer_organization_id,
    x.performer_department_id,
    x.recommended_receiving_start_time,
    x.recommended_receiving_end_time,
    coalesce(x."description", '') as "description",
    coalesce(x.patient_instruction, '') as patient_instruction,
    coalesce(x.group_number, '') as group_number,
    x.ehealth_id
from ehe_outgoing_medical_referral x
where x.record_state <> 4