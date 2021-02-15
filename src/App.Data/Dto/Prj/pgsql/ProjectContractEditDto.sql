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
	pc.doc_state, 
	pc.reg_date, 
	pc.reg_number, 
	pc."cost", 
	pc.bidding_type,
	pc.tender_code,
	pc.bidding_code,
	pc.bidding_subject,
	pc.description
FROM
	prj_contract AS pc
INNER JOIN project ON pc.project_id = project.id
INNER JOIN organization AS customer ON pc.customer_id = customer.id
INNER JOIN organization AS generalContractor ON pc.general_contractor_id = generalContractor.id
LEFT JOIN cdn_type_of_project_work AS ctopw ON project.type_of_project_work_id = ctopw.id
WHERE pc.record_state <> 4