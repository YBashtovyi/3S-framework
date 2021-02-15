SELECT
	project."id", 
	project.created_on,
	project.owner_id,
	project.created_by,
	COALESCE(cp.caption, '') as created_full_name,
	project.code,
	COALESCE(org.full_name, '') as owner_name,
	COALESCE(project.project_status, '') as project_status, 
	COALESCE(prg_status.name, '') as project_status_name,
	project.region_id,
	COALESCE(region.name, '') as region_name,
	project.district_id,
	COALESCE(district.name, '') as district_name,
	COALESCE(project."name", '') as name,
	COALESCE(project.full_name, '') as full_name,
	project."cost", 
	project.date_begin, 
	project.date_end, 
	COALESCE(project.project_implementation_state, '') as project_implementation_state,
	COALESCE(impl_state.name, '') as project_implementation_state_name,
	COALESCE(project.type_of_financing, '') as type_of_financing,
	COALESCE(type_financing.name, '') as type_of_financing_name,
	pco.construction_object_id,
	COALESCE(co.name, '') as construction_object_name,
	project.type_of_project_work_id,
	COALESCE(ctopw.code, '') as type_of_project_work_code,
	COALESCE(ctopw.name, '') as type_of_project_work_name,
	project.repair_square,
	project.repair_length,
	project."description",
	project.atu_coordinates
FROM
	project
INNER JOIN cmn_enum_record as prg_status ON project.project_status = prg_status.code and prg_status."group" = 'ProjectStatus'
INNER JOIN cmn_enum_record as impl_state ON project.project_implementation_state = impl_state.code and impl_state."group" = 'ProjectImplementationState'
INNER JOIN cmn_enum_record as type_financing ON project.type_of_financing = type_financing.code and type_financing."group" = 'TypeOfFinancing'
INNER JOIN atu_region as region ON project.region_id = region.id
LEFT JOIN atu_district as district ON project.district_id = district.id
INNER JOIN organization as org ON project.owner_id = org.id
INNER JOIN prj_construction_object AS pco ON project.id = pco.project_id
INNER JOIN construction_object AS co ON pco.construction_object_id = co.id
INNER JOIN cdn_type_of_project_work AS ctopw ON project.type_of_project_work_id = ctopw.id
-- GET PERSON DATA
INNER JOIN adm_user AS "user" ON project.created_by = "user"."id"
INNER JOIN org_employee AS oe ON "user".employee_id = oe.id
INNER JOIN cmn_person AS cp ON oe.person_id = cp.id
WHERE project.record_state <> 4