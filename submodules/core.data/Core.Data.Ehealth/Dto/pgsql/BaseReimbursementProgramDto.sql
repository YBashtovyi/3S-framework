SELECT x.id,
    x.caption,
    x.code,
    x.start_date,
    x.end_date,
    x.activity_status_id,
    x.description,
    x.is_ehealth_program,
    x.ehealth_medical_service_program_id,
    x.medicine_search_type_id,
    x.prescription_max_items_count,
    x.max_course_duration,
    x.is_pharmacy_desirable,
    x.prohibit_saving_same_inn
FROM ehp_reimbursement_programs x
WHERE x.record_state <> 4