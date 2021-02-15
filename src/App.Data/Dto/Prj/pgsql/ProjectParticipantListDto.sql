SELECT
	pp."id",
	pp.created_on,
	pp.project_id,
	pp.project_role,
	COALESCE ( role.NAME, '' ) AS project_role_name,
	pp.participant_id,
	COALESCE ( org."name", '' ) AS participant_name,
	pp.responsible_person_id,
	COALESCE ( person.caption, '' ) AS responsible_person_full_name,
	COALESCE ( pp.description, '' ) AS description 
FROM
	prj_participant AS pp
	INNER JOIN cmn_enum_record AS role ON pp.project_role = role.code AND role."group" = 'ProjectRole'
	INNER JOIN organization AS org ON pp.participant_id = org.ID AND org.record_state <> 4
	INNER JOIN org_employee AS oe ON pp.responsible_person_id = oe.ID AND oe.record_state <> 4
	INNER JOIN cmn_person AS person ON oe.person_id = person.ID AND person.record_state <> 4 
WHERE
	pp.record_state <> 4