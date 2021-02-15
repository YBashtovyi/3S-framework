SELECT
	x."id",
	x.created_on,
	x.record_state,
	x."name",
	x."group",
	COALESCE ( x.group_name, '' ) AS group_name,
	x.code,
	COALESCE ( x."value", '' ) AS "value",
	x.item_number 
FROM
	cmn_enum_record x 
WHERE
	x.record_state != 4