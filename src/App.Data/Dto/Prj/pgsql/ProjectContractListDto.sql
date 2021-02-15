SELECT
	pc."id", 
	pc.created_on, 
	pc.doc_state, 
	docState."name" AS doc_state_name,
	pc.reg_date, 
	pc.reg_number, 
	pc.doc_type, 
	docType."name" AS doc_type_name,
	pc.project_id, 
	project."name" AS project_name,
	pc."cost", 
	pc.customer_id, 
	customer."name" AS customer_name,
	pc.general_contractor_id,
	generalContractor."name" AS general_contractor_name
FROM
	prj_contract AS pc
INNER JOIN project ON pc.project_id = project.id
INNER JOIN cmn_enum_record AS docType ON pc.doc_type = docType.code AND docType."group" = 'DocType'
INNER JOIN cmn_enum_record AS docState ON pc.doc_state = docState.code AND docState."group" = 'DocState'
INNER JOIN organization AS customer ON pc.customer_id = customer.id
INNER JOIN organization AS generalContractor ON pc.general_contractor_id = generalContractor.id
WHERE pc.record_state <> 4