SELECT x.id,
    x.caption,
	COALESCE(x.type_code, '') as type_code,
	COALESCE(x.institution_name, '') as institution_name,
	COALESCE(x.speciality_code, '') as speciality_code,
    COALESCE(x.issued_date, CURRENT_TIMESTAMP) as issued_date, 
    COALESCE(x.certificate_number, '') as certificate_number,
    COALESCE(x.valid_to, CURRENT_TIMESTAMP) as valid_to,
    COALESCE(x.additional_info, '') as additional_info,
    x.entity_id
FROM ehd_qualification x
WHERE x.record_state <> 4