SELECT x.id,
    x.caption,
	COALESCE(x."name", '') as "name",
    x.is_active,    
	COALESCE(x.code, '') as code,
	COALESCE(x."value", '') as "value"
FROM ehd_dictionary x
WHERE x.record_state <> 4