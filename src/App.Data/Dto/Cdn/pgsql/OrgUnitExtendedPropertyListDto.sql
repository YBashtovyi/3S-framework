SELECT
	prop."id",
	prop.created_on,
	prop.org_unit_id,
	prop.org_extended_property,
	COALESCE ( cer.name, '' ) AS org_extended_property_name,
	COALESCE ( prop."value", '' ) AS "value",
	prop.value_json 
FROM
	org_unit_extended_property AS prop
	INNER JOIN cmn_enum_record AS cer ON prop.org_extended_property = cer.code AND cer."group" = 'OrgExtendedProperty' 
WHERE
	prop.record_state <> 4