SELECT
	empl."id", 
    empl.created_on,
	empl.person_id,
	COALESCE(cp.caption, '') as person_full_name
FROM
	org_employee AS empl
INNER JOIN cmn_person AS cp ON empl.person_id = cp.id AND cp.record_state <> 4
WHERE empl.record_state <> 4