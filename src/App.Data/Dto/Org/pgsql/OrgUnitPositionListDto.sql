SELECT
	oup."id",
	oup.created_on,
	oup.org_unit_id,
	oup.position_id,
	COALESCE ( pos.NAME, '' ) AS position_name,
	oup.staff_unit_count,
	COALESCE ( oup.description, '' ) AS description 
FROM
	org_unit_position AS oup
	INNER JOIN cdn_position AS pos ON oup.position_id = pos.ID 
WHERE
	oup.record_state <> 4