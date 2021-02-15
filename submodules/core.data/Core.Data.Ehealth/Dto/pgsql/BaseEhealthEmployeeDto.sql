SELECT x.id,
    x.caption,
    COALESCE(x.division_id, uuid_in('00000000-0000-0000-0000-000000000000')) as division_id,
    COALESCE(x.legal_entity_id, uuid_in('00000000-0000-0000-0000-000000000000')) as legal_entity_id,
    COALESCE(x.position_code, '') as position_code,
    COALESCE(x."start_date", CURRENT_TIMESTAMP) as "start_date", 
    COALESCE(x."end_date", CURRENT_TIMESTAMP) as "end_date", 
	COALESCE(x.employee_type_code, '') as employee_type_code,
    COALESCE(x.ehealth_id, uuid_in('00000000-0000-0000-0000-000000000000')) as ehealth_id,
    COALESCE(x.ehealth_request_id, uuid_in('00000000-0000-0000-0000-000000000000')) as ehealth_request_id
FROM ehd_employee x
WHERE x.record_state <> 4