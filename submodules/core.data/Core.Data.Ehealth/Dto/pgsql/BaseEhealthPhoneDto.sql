SELECT x.id,
    ed.caption,
	COALESCE(x.type_code, '') as type_code,
	COALESCE(x."number", '') as "number",
	x.entity_id
FROM ehd_phone x
  LEFT JOIN ehd_dictionary AS ed on x.type_code = ed.code AND ed.is_active = true and ed."name" = 'PHONE_TYPE'
WHERE x.record_state <> 4