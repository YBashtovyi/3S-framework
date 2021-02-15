SELECT
	dep."id",
	COALESCE ( dep."name", '' ) AS name,
	COALESCE ( dep.full_name, '' ) AS full_name,
	COALESCE ( dep.code, '' ) AS code,
	COALESCE ( dep."description", '' ) AS description, 
	dep.department_type,
	dep.department_state,
	dep.parent_id
FROM
	org_department AS dep
WHERE dep.record_state <> 4