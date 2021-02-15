SELECT
	ar.id, 
	ar.right_type, 
	COALESCE(cer.name, '') as right_type_name,
	ar.code,
	ar.description,
	ar.name
FROM
	adm_right AS ar
LEFT JOIN cmn_enum_record AS cer ON ar.right_type = cer.code AND cer."group" = 'RightType'
WHERE ar.record_state <> 4