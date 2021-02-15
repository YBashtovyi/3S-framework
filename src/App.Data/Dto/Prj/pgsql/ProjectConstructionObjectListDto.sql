SELECT
	project."id", 
	project.created_on,
	COALESCE(project.code, '') as code, 
	COALESCE(project.project_status, '') as project_status, 
	COALESCE(prg_status.name, '') as project_status_name,
	project.region_id,
	COALESCE(region.name, '') as region_name,
	COALESCE(project."name", '') as name, 
	COALESCE(project.project_implementation_state, '') as project_implementation_state,
	COALESCE(impl_state.name, '') as project_implementation_state_name,
	project.date_begin, 
	project.date_end, 
	pco.construction_object_id
FROM
	project
INNER JOIN cmn_enum_record as prg_status ON project.project_status = prg_status.code and prg_status."group" = 'ProjectStatus'
INNER JOIN cmn_enum_record as impl_state ON project.project_implementation_state = impl_state.code and impl_state."group" = 'ProjectImplementationState'
INNER JOIN atu_region as region ON project.region_id = region.id
INNER JOIN prj_construction_object as pco ON project.id = pco.project_id
WHERE project.record_state <> 4
	