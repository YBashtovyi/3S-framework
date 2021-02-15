SELECT
	x."id",
	x.created_on,
	x.record_state,
	x."name",
	x."group",
	x.group_name,
	x.code,
	x."value",
	x.item_number 
FROM
	cmn_enum_record x 
WHERE
	x.record_state != 4