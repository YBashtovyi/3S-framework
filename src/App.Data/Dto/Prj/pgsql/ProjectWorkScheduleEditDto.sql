SELECT
	pws."id", 
	pws.project_id, 
	pws.doc_type,
	pws.reg_date, 
	pws.reg_number, 
	pws.doc_state,
	pws.description,
	pws.parent_id
FROM
	prj_work_schedule AS pws
WHERE pws.record_state <> 4