SELECT
	oe."id", 
    oe.record_state,
	oe.person_id,
	oup.org_unit_id,
	au.id as user_id
FROM
	org_employee AS oe
INNER JOIN adm_user As au ON oe.id = au.employee_id
INNER JOIN org_unit_staff AS ous ON oe."id" = ous.employee_id
INNER JOIN org_unit_position AS oup ON ous.org_unit_position_id = oup.id