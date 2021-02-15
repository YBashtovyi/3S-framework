SELECT
	ctopw."id", 
	ctopw.code, 
	ctopw."name", 
	ctopw.full_name, 
	ctopw.parent_id
FROM
	cdn_type_of_project_work AS ctopw
WHERE ctopw.record_state <> 4