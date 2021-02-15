SELECT
	obj."id",
	obj.code,
	COALESCE ( obj.object_status, '' ) AS object_status,
	COALESCE ( obj."name", '' ) AS "name",
	COALESCE ( obj.full_name, '' ) AS full_name,
	COALESCE ( obj.type_of_construction_object, '' ) AS type_of_construction_object,
	COALESCE ( obj.class_of_consequence, '' ) AS class_of_consequence,
	obj.atu_coordinates,
	obj."description"
FROM
	construction_object AS obj
WHERE obj.record_state <> 4