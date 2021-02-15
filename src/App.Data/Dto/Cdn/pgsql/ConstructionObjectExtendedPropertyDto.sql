SELECT
	coep."id", 
	coep.construction_object_id, 
	coep.created_on, 
	coep.dictionary_id, 
	coep."value", 
	coep.value_json
FROM
	construction_object_extended_property AS coep
WHERE coep.record_state <> 4