SELECT x.id,
    x.caption,
	COALESCE(x.first_name, '') as first_name,
	COALESCE(x.last_name, '') as last_name,
	COALESCE(x.second_name, '') as second_name
FROM ehd_person x
WHERE x.record_state <> 4