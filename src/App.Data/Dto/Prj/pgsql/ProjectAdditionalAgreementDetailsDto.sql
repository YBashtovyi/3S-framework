SELECT
	paa."id", 
	paa.project_id,
	project."name" AS project_name,
	paa.parent_id,
	pc.reg_date AS parent_reg_date,
	pc.reg_number AS parent_reg_number,
	pc.customer_id,
	customer."name" AS customer_name,
	pc.general_contractor_id,
	generalContractor."name" AS general_contractor_name,
	ctopw.code AS type_of_project_work_code,
	ctopw."name" AS type_of_project_work_name,
	paa.doc_type,
	docType."name" AS doc_type_name,
	paa.doc_state, 
	docState."name" AS doc_state_name,
	paa.reg_date, 
	paa.reg_number, 
	paa."cost", 
	paa.description
FROM
	prj_additional_agreement AS paa
INNER JOIN cmn_enum_record AS docType ON paa.doc_type = docType.code AND docType."group" = 'DocType'
INNER JOIN cmn_enum_record AS docState ON paa.doc_state = docState.code AND docState."group" = 'DocState'
INNER JOIN project ON paa.project_id = project.id
INNER JOIN prj_contract AS pc ON paa.parent_id = pc.id
INNER JOIN organization AS customer ON pc.customer_id = customer."id"
INNER JOIN organization AS generalContractor ON pc.general_contractor_id = generalContractor.id
LEFT JOIN cdn_type_of_project_work AS ctopw ON project.type_of_project_work_id = ctopw.id
WHERE paa.record_state <> 4