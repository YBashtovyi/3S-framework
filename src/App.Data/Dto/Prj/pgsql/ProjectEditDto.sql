SELECT
	project."id",
	project.owner_id,
	COALESCE ( project.code, '' ) AS code,
	COALESCE ( project."name", '' ) AS "name",
	COALESCE(project.full_name, '') as full_name,
	project.region_id,
	project.district_id,
	COALESCE ( project.project_status, '' ) AS project_status,
	COALESCE ( project.type_of_financing, '' ) AS type_of_financing,
	COALESCE ( project.project_implementation_state, '' ) AS project_implementation_state,
	project."cost",
	project.date_begin,
	project.date_end,
	pco.construction_object_id,
	project.type_of_project_work_id,
	project.repair_square,
	project.repair_length,
	project."description"
FROM
	project 
INNER JOIN prj_construction_object AS pco ON project.id = pco.project_id
WHERE
	project.record_state <> 4