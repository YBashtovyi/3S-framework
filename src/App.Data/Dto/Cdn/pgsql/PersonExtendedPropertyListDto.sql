SELECT
	prop."id",
	prop.created_on,
	prop.person_id,
	prop.person_extended_property,
	COALESCE ( cer.name, '' ) AS person_extended_property_name,
	COALESCE ( prop."value", '' ) AS "value",
	prop.value_json 
FROM
	cmn_person_extended_property AS prop
	INNER JOIN cmn_enum_record AS cer ON prop.person_extended_property = cer.code AND cer."group" = 'PersonExtendedProperty' 
WHERE
	prop.record_state <> 4