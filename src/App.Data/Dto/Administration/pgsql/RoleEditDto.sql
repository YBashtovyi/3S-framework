SELECT
	role."id", 
	role.created_on, 
	role."name", 
	role.code, 
	role.is_active,
	role.description
FROM
	adm_role AS "role"
WHERE role.record_state <> 4