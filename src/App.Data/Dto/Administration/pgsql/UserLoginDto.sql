SELECT
	au."id",
	au.employee_id,
	COALESCE ( au."login", '' ) AS LOGIN 
FROM
	adm_user AS au 
WHERE
	au.record_state <> 4