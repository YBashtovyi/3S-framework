SELECT x.id,
    x.caption,
	COALESCE(x.type_code, '') as type_code,
	COALESCE(x."number", '') as "number",
	COALESCE(x.issued_by, '') as issued_by,
	x.issued_at, 
	x.entity_id
FROM ehd_document x
WHERE x.record_state <> 4