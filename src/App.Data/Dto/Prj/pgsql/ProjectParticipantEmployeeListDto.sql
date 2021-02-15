SELECT
	p.id,
	org.name as organization_name,
	ous.employee_id,
	--pos."name" as position_name,
	cp.caption as employee_full_name
FROM
	project as p
INNER JOIN prj_participant as pp ON p.id = pp.project_id
INNER JOIN organization as org ON pp.participant_id = org."id"
INNER JOIN org_unit_position as oup ON org.id = oup.org_unit_id
--LEFT JOIN cdn_position as pos ON oup.position_id = pos.id
INNER JOIN org_unit_staff as ous ON oup.id = ous.org_unit_position_id
INNER JOIN org_employee as oe on ous.employee_id = oe.id
INNER JOIN cmn_person as cp on oe.person_id = cp.id
WHERE p.record_state <> 4
GROUP BY 1,2,3,4