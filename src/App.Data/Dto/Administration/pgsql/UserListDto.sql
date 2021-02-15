SELECT
	au.id,
	oe.created_on, 
	au.employee_id,
	coalesce(cp.first_name, '') as person_first_name,
	coalesce(cp.middle_name, '') as person_middle_name,
	coalesce(cp.last_name, '') as person_last_name,
	coalesce(ou."name", '') as org_unit_name,
	coalesce(string_agg(pos.name, ', '), '') as org_unit_position_name
FROM
	adm_user as au
JOIN org_employee as oe on au.employee_id = oe.id and oe.record_state <> 4
JOIN cmn_person as cp on oe.person_id = cp.id and cp.record_state <> 4
LEFT JOIN org_unit_staff as ous on au.employee_id = ous.employee_id
LEFT JOIN org_unit_position as oup on ous.org_unit_position_id = oup.id
LEFT JOIN org_unit as ou on oup.org_unit_id = ou.id
LEFT JOIN cdn_position as pos on oup.position_id = pos.id
WHERE au.record_state <> 4
GROUP BY 1,2,3,4,5,6,7