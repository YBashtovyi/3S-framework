SELECT
	coepd."id", 
	coepd.created_on, 
	coepd.code, 
	coepd."name", 
	coepd.data_format,
	COALESCE(cer.name, '') as data_format_name,
	coepd.parent_id,
	COALESCE(parent."name", '') as parent_name
FROM
	construction_object_ex_property_dictionary AS coepd
LEFT JOIN cmn_enum_record as cer on coepd.data_format = cer.code and cer."group" = 'DataFormat'
LEFT JOIN construction_object_ex_property_dictionary as parent on coepd.parent_id = parent.id
WHERE coepd.record_state <> 4