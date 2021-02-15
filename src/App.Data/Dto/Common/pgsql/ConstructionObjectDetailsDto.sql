SELECT
	obj."id",
	obj.code,
	COALESCE ( obj.object_status, '' ) AS object_status,
	COALESCE ( obj_status."name", '' ) AS object_status_name,
	COALESCE ( obj."name", '' ) AS "name",
	COALESCE ( obj.full_name, '' ) AS full_name,
	COALESCE ( obj.type_of_construction_object, '' ) AS type_of_construction_object,
	COALESCE ( coepd."name", '' ) AS type_of_construction_object_name,
	coepd.id AS type_of_construction_object_id,
	COALESCE ( obj.class_of_consequence, '' ) AS class_of_consequence,
	COALESCE ( coc."name", '' ) AS class_of_consequence_name,
	obj.atu_coordinates,
	obj."description"
FROM
	construction_object AS obj
	LEFT JOIN cmn_enum_record AS obj_status ON obj.object_status = obj_status.code AND obj_status."group" = 'ObjectStatus'
	LEFT JOIN cmn_enum_record AS coc ON obj.class_of_consequence = coc.code AND coc."group" = 'ClassOfConsequence'
	LEFT JOIN construction_object_ex_property_dictionary AS coepd ON obj.type_of_construction_object = coepd.code
WHERE obj.record_state <> 4