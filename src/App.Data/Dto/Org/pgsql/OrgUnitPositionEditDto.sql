SELECT
	oup."id",
	oup.org_unit_id,
	oup.position_id,
	oup.staff_unit_count,
	COALESCE ( oup.description, '' ) AS description 
FROM
	org_unit_position AS oup
WHERE
	oup.record_state <> 4