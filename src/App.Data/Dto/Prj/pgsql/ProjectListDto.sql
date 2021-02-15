SELECT 
	project."id", 
	project.owner_id,
  project.created_on,
	COALESCE(project.code, '') as code, 
	obj.code as construction_object_code,
	COALESCE(project.project_status, '') as project_status, 
	COALESCE(prg_status.name, '') as project_status_name,
	project.region_id,
	COALESCE(region.name, '') as region_name,
	COALESCE(project."name", '') as name, 
	project."cost", 
	project.date_begin, 
	project.date_end, 
	COALESCE(project.project_implementation_state, '') as project_implementation_state,
	COALESCE(impl_state.name, '') as project_implementation_state_name,
	COALESCE(project.type_of_financing, '') as type_of_financing,
	COALESCE(tp_financ.name, '') as type_of_financing_name,
	project.atu_coordinates::jsonb,
	project.type_of_project_work_id,
	COALESCE(ctopw.code, '') as type_of_project_work_code,
	COALESCE(ctopw.name, '') as type_of_project_work_name,
	org.participant_org_name,
	genOrg."name" as general_contractor_name,
	cutomerOrg."name" as customer_name
FROM
	project
INNER JOIN prj_construction_object as prjObj ON project.id = prjObj.project_id
INNER JOIN construction_object as obj ON prjObj.construction_object_id = obj.id
INNER JOIN cmn_enum_record as prg_status ON project.project_status = prg_status.code and prg_status."group" = 'ProjectStatus'
INNER JOIN cmn_enum_record as impl_state ON project.project_implementation_state = impl_state.code and impl_state."group" = 'ProjectImplementationState'
INNER JOIN cmn_enum_record as tp_financ ON project.type_of_financing = tp_financ.code and tp_financ."group" = 'TypeOfFinancing'
INNER JOIN cdn_type_of_project_work AS ctopw ON project.type_of_project_work_id = ctopw.id
INNER JOIN atu_region as region ON project.region_id= region.id
LEFT JOIN ( SELECT participant.project_id, string_agg(org.name, ', ') as participant_org_name FROM prj_participant as participant 
						LEFT JOIN organization as org ON participant.participant_id = org.id
						GROUP BY participant.project_id) org ON project.id = org.project_id
-- GENERAL CONTRACTOR OF THE PROJECT
LEFT JOIN prj_participant as genPart ON project.id = genPart.project_id AND genPart.project_role = 'GeneralContractor'
LEFT JOIN organization as genOrg ON genPart.participant_id = genOrg.id
-- CUSTOMER
LEFT JOIN prj_participant as customer ON project.id = customer.project_id AND customer.project_role = 'Customer'
LEFT JOIN organization as cutomerOrg ON customer.participant_id = cutomerOrg.id
WHERE project.record_state <> 4
	