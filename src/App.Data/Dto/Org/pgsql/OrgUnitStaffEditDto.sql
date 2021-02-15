SELECT
	ous."id",
	ous.created_on,
	ous.org_unit_position_id,
	ous.employee_id,
    oe.person_id,
    COALESCE(cp.caption, '') AS person_full_name,
	ous.start_date,
	ous.end_date 
FROM
	org_unit_staff AS ous
	INNER JOIN org_employee AS oe ON ous.employee_id = oe.ID AND oe.record_state <> 4
	INNER JOIN cmn_person AS cp ON oe.person_id = cp.ID AND cp.record_state <> 4 
WHERE
	ous.record_state <> 4