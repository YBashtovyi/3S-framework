SELECT
	obj."id",
	obj.created_on,
	obj.code,
	COALESCE ( obj.object_status, '' ) AS object_status,
	COALESCE ( obj_status."name", '' ) AS object_status_name,
	COALESCE ( obj."name", '' ) AS "name",
	COALESCE ( obj.type_of_construction_object, '' ) AS type_of_construction_object,
	COALESCE ( coepd."name", '' ) AS type_of_construction_object_name,
	COALESCE ( obj.class_of_consequence, '' ) AS class_of_consequence,
	COALESCE ( coc."name", '' ) AS class_of_consequence_name,
	(
		SELECT 
			string_agg(coepd.name || ' - ' || coepd2.name, ', ')
		FROM construction_object_extended_property AS coep 
		INNER JOIN construction_object_ex_property_dictionary AS coepd ON (coep.value_json->> 'constructionObjectExPropertyId')::uuid = coepd.id
		LEFT JOIN construction_object_ex_property_dictionary AS coepd2 ON (coep.value_json->> 'constructionObjectSubExPropertyId')::uuid = coepd2.id WHERE coep.construction_object_id = obj.id) AS extended_property,
	obj.atu_coordinates 
FROM
	construction_object AS obj
	LEFT JOIN cmn_enum_record AS obj_status ON obj.object_status = obj_status.code AND obj_status."group" = 'ObjectStatus'
	LEFT JOIN cmn_enum_record AS coc ON obj.class_of_consequence = coc.code AND coc."group" = 'ClassOfConsequence'
	LEFT JOIN construction_object_ex_property_dictionary AS coepd ON obj.type_of_construction_object = coepd.code
WHERE obj.record_state <> 4