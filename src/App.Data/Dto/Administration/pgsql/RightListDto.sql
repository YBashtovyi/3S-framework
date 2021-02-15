SELECT
	id, 
	created_on, 
	right_type, 
	code,
	name
FROM
	adm_right
WHERE adm_right.record_state <> 4