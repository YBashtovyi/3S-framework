SELECT
	pws."id", 
	pws.project_id, 
	pws.doc_type,
	"type"."name" AS doc_type_name,
	pws.reg_date, 
	pws.reg_number, 
	pws.doc_state,
	"state"."name" AS doc_state_name,
	pws.description,
	pws.parent_id,
	pws_parent.reg_date AS parent_reg_date,
	pws_parent.reg_number AS parent_reg_number
FROM
	prj_work_schedule AS pws
LEFT JOIN prj_work_schedule AS pws_parent ON pws.parent_id = pws_parent.id
INNER JOIN cmn_enum_record AS "type" ON pws.doc_type = "type".code AND "type"."group" = 'DocType'
INNER JOIN cmn_enum_record AS "state" ON pws.doc_state = "state".code AND "state"."group" = 'DocState'
WHERE pws.record_state <> 4