SELECT
	coep."id",
	coep.created_on,
	coep.construction_object_id,
	coep."value",
	coep.value_json ->> 'description' AS description,
	(coep.value_json->> 'constructionObjectExPropertyId')::uuid AS construction_object_ex_property_id,
	coepd.code AS construction_object_ex_property_code,
	coepd.name AS construction_object_ex_property_name,
	(coep.value_json->> 'constructionObjectSubExPropertyId')::uuid AS construction_object_sub_ex_property_id,
	coepd2.code AS construction_object_sub_ex_property_code,
	coepd2.name AS construction_object_sub_ex_property_name
FROM
	"construction_object_extended_property" AS coep
INNER JOIN construction_object_ex_property_dictionary AS coepd ON (coep.value_json->> 'constructionObjectExPropertyId')::uuid = coepd.id
LEFT JOIN construction_object_ex_property_dictionary AS coepd2 ON (coep.value_json->> 'constructionObjectSubExPropertyId')::uuid = coepd2.id
WHERE coep.record_state <> 4