SELECT
	paa."id", 
	paa.project_id,
	paa.parent_id,
	paa.doc_type,
	paa.doc_state, 
	paa.reg_date, 
	paa.reg_number, 
	paa."cost", 
	paa.description
FROM
	prj_additional_agreement AS paa
WHERE paa.record_state <> 4