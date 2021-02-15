SELECT
	coepd."id", 
	coepd.created_on, 
	coepd.code, 
	coepd."name", 
	coepd.full_name,
	coepd.data_format,
	coepd.parent_id
FROM
	construction_object_ex_property_dictionary AS coepd
WHERE coepd.record_state <> 4