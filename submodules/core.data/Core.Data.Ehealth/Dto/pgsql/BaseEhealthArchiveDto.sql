SELECT x.id, 
    x.caption,
	COALESCE(x."date", CURRENT_TIMESTAMP) as "date", 
	COALESCE(x.place, '') as place,
	x.entity_id
FROM ehd_archive x
WHERE x.record_state <> 4