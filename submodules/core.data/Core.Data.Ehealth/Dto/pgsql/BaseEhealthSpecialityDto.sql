SELECT x.id,
    x.caption,
	COALESCE(x.speciality_code, '') as speciality_code,
	speciality_officio,
	COALESCE(x.level_code, '') as level_code,
	COALESCE(x.qualification_type_code, '') as qualification_type_code,
    COALESCE(x.attestation_name, '') as attestation_name,
    x.attestation_date,
    x.valid_to_date,
    COALESCE(x.certificate_number, '') as certificate_number,
    x.entity_id
FROM ehd_speciality x
WHERE x.record_state <> 4