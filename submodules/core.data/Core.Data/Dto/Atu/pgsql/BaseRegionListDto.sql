SELECT
	atu_region."id", 
	atu_region.parent_id, 
	COALESCE(atu_region.code, '') as code,
	COALESCE(atu_region.atu_region_type, '') as atu_region_type,
	COALESCE(atu_region.koatu, '') as koatu, 
	COALESCE(atu_region."name", '') as name
FROM
	atu_region
WHERE atu_region.record_state <> 4