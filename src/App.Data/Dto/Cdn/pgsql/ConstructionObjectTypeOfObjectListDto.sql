SELECT
	coepd."id", 
	coepd.code, 
	coepd."name", 
	coepd.parent_id
FROM
	construction_object_ex_property_dictionary AS coepd
WHERE coepd.record_state <> 4