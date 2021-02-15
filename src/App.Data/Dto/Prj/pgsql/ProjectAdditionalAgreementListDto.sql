SELECT
	paa."id", 
	paa.created_on,  
	paa.reg_date, 
	paa.reg_number, 
	paa.doc_state, 
	docState."name" AS doc_state_name,
	paa."cost", 
	paa.parent_id
FROM
	prj_additional_agreement AS paa
INNER JOIN cmn_enum_record AS docState ON paa.doc_state = docState.code AND docState."group" = 'DocState'
WHERE paa.record_state <> 4