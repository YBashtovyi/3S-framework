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
	generalContractor."name" AS general_contractor_name,
	null as parent_id
FROM
	prj_contract AS pc
INNER JOIN project ON pc.project_id = project.id
INNER JOIN cmn_enum_record AS docType ON pc.doc_type = docType.code AND docType."group" = 'DocType'
INNER JOIN cmn_enum_record AS docState ON pc.doc_state = docState.code AND docState."group" = 'DocState'
INNER JOIN organization AS customer ON pc.customer_id = customer.id
INNER JOIN organization AS generalContractor ON pc.general_contractor_id = generalContractor.id
WHERE pc.record_state <> 4

UNION

SELECT
	paa."id", 
	paa.created_on, 
	paa.doc_state, 
	docState."name" AS doc_state_name,	
	paa.reg_date, 
	paa.reg_number,
	paa.doc_type, 
	docType."name" AS doc_type_name,
	paa.project_id,
	project."name" AS project_name,
	paa."cost", 
	pc.customer_id, 
	customer."name" AS customer_name,
	pc.general_contractor_id,
	generalContractor."name" AS general_contractor_name,
	paa.parent_id
FROM
	prj_additional_agreement AS paa
INNER JOIN project ON paa.project_id = project.id
INNER JOIN cmn_enum_record AS docType ON paa.doc_type = docType.code AND docType."group" = 'DocType'
INNER JOIN cmn_enum_record AS docState ON paa.doc_state = docState.code AND docState."group" = 'DocState'
INNER JOIN prj_contract AS pc ON paa.parent_id = pc.id
INNER JOIN organization AS customer ON pc.customer_id = customer."id"
INNER JOIN organization AS generalContractor ON pc.general_contractor_id = generalContractor.id
WHERE paa.record_state <> 4