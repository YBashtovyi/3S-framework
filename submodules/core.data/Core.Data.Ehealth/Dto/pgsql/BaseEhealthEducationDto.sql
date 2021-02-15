SELECT x.id,
    x.caption,
	COALESCE(x.country_code, '') as country_code,
	COALESCE(x.city, '') as city,
	COALESCE(x.institution_name, '') as institution_name,
    COALESCE(x.issued_date, CURRENT_TIMESTAMP) as issued_date,
	COALESCE(x.diploma_number, '') as diploma_number,
   	COALESCE(x.degree_code, '') as degree_code,
    COALESCE(x.speciality_code, '') as speciality_code,
    x.entity_id
FROM ehd_education x
WHERE x.record_state <> 4