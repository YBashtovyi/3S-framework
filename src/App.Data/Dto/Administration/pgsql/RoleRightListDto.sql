SELECT
	arr.id,
	role.id AS role_id,
	ar.id AS right_id,
	ar.code,
	ar.NAME 
FROM
	adm_role AS role
	INNER JOIN adm_role_right AS arr ON role.ID = arr.role_id AND arr.record_state <> 4
	INNER JOIN adm_right AS ar ON arr.right_id = ar.id AND ar.record_state <> 4
WHERE role.record_state <> 4