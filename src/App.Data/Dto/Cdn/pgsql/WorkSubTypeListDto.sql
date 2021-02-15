SELECT
	cwst."id", 
	cwst.created_on, 
	cwst.code, 
	cwst."name", 
	cwst.measurement_unit, 
	mu."name" AS measurement_unit_name,
	mu."value" AS measurement_unit_value,
	cwst.classifier_type, 
	ct."name" AS classifier_type_name,
	cwst.is_active, 
	cwst.parent_id,
	COALESCE(parent."name", '') parent_name,
	COALESCE(parent.code, '') parent_code
FROM
	cdn_work_sub_type AS cwst
LEFT JOIN cdn_work_sub_type AS parent ON cwst.parent_id = parent."id"
INNER JOIN cmn_enum_record AS mu ON cwst.measurement_unit = mu.code AND mu."group" = 'MeasurementUnit'
INNER JOIN cmn_enum_record AS ct ON cwst.classifier_type = ct.code AND ct."group" = 'ClassifierType'
WHERE cwst.record_state <> 4