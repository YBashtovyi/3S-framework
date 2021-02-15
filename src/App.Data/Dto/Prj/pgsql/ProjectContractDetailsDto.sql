SELECT
	pc."id", 
	pc.created_on, 
	pc.project_id, 
	project."name" AS project_name,
	pc.customer_id, 
	customer."name" AS customer_name,
	pc.general_contractor_id,
	generalContractor."name" AS general_contractor_name,
	ctopw.code AS type_of_project_work_code,
	ctopw."name" AS type_of_project_work_name,
	pc.doc_type, 
	docType."name" AS doc_type_name,
	pc.doc_state, 
	docState."name" AS doc_state_name,
	pc.reg_date, 
	pc.reg_number, 
	pc."cost", 
	pc.bidding_type,
	biddingType."name" AS bidding_type_name,
	pc.tender_code,
	pc.bidding_code,
	pc.bidding_subject,
	pc.description
FROM
	prj_contract AS pc
INNER JOIN project ON pc.project_id = project.id
INNER JOIN cmn_enum_record AS docType ON pc.doc_type = docType.code AND docType."group" = 'DocType'
INNER JOIN cmn_enum_record AS docState ON pc.doc_state = docState.code AND docState."group" = 'DocState'
INNER JOIN cmn_enum_record AS biddingType ON pc.bidding_type = biddingType.code AND biddingType."group" = 'BiddingType'
INNER JOIN organization AS customer ON pc.customer_id = customer.id
INNER JOIN organization AS generalContractor ON pc.general_contractor_id = generalContractor.id
LEFT JOIN cdn_type_of_project_work AS ctopw ON project.type_of_project_work_id = ctopw.id
WHERE pc.record_state <> 4