SELECT
	dep."id", 
	dep.created_on, 
	dep."name", 
	dep.full_name, 
	dep.code, 
	dep."description", 
	dep.department_type,
	cer."name" as department_type_name,
	dep.department_state,
	depState."name" as department_state_name,	
	org."name" as parent_name
FROM
	org_department AS dep
LEFT JOIN organization as org on dep.parent_id = org."id"
JOIN cmn_enum_record as cer on dep.department_type = cer.code and cer."group" = 'DepartmentType'
JOIN cmn_enum_record as depState on dep.department_state = depState.code and depState."group" = 'DepartmentState'
WHERE dep.record_state <> 4