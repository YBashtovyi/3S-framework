SELECT
	photo."id", 
	photo.created_on, 
	photo.project_id, 
	project."name" AS project_name,
	photo.customer_id, 
	customer."name" AS customer_name,
	photo.general_contractor_id, 
	generalContractor."name" AS general_contractor_name,
	photo.doc_type, 
	docType."name" AS doc_type_name,
	photo.doc_state, 
	docState."name" AS doc_state_name,
	photo.reg_date, 
	photo.reg_number, 
	photo.fixation_type, 
	fixType."name" AS fixation_type_name, 
	photo.fixation_state,
	fixState."name" AS fixation_state_name,
	photo.responsible_employee_id,
	person.caption AS responsible_employee_full_name,
	photo.description,
	ctopw.code AS type_of_project_work_code,
	ctopw."name" AS type_of_project_work_name,
	photo.atu_coordinates
FROM
	prj_photo_report AS photo
INNER JOIN project ON photo.project_id = project.id
INNER JOIN cmn_enum_record AS docType ON photo.doc_type = docType.code AND docType."group" = 'DocType'
INNER JOIN cmn_enum_record AS docState ON photo.doc_state = docState.code AND docState."group" = 'DocState'
INNER JOIN cmn_enum_record AS fixType ON photo.fixation_type = fixType.code AND fixType."group" = 'FixationType'
INNER JOIN cmn_enum_record AS fixState ON photo.fixation_state = fixState.code AND fixState."group" = 'FixationState'
LEFT JOIN cdn_type_of_project_work AS ctopw ON project.type_of_project_work_id = ctopw.id
INNER JOIN organization AS customer ON photo.customer_id = customer.id
INNER JOIN organization AS generalContractor ON photo.general_contractor_id = generalContractor.id
INNER JOIN org_employee AS empl ON photo.responsible_employee_id = empl."id"
INNER JOIN cmn_person AS person ON empl.person_id = person."id"
WHERE photo.record_state <> 4