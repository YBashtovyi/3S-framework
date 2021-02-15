SELECT
	cwst."id", 
	cwst.code, 
	cwst."name", 
	cwst.measurement_unit,
	cwst.classifier_type, 
	cwst.is_active, 
	cwst.parent_id
FROM
	cdn_work_sub_type AS cwst
WHERE cwst.record_state <> 4