SELECT
	pws."id", 
	pws.project_id,
	pws.created_on, 
	pws.doc_type,
	"type"."name" AS doc_type_name,
	pws.reg_date, 
	pws.reg_number, 
	pws.doc_state,
	"state"."name" AS doc_state_name
FROM
	prj_work_schedule AS pws
INNER JOIN cmn_enum_record AS "type" ON pws.doc_type = "type".code AND "type"."group" = 'DocType'
INNER JOIN cmn_enum_record AS "state" ON pws.doc_state = "state".code AND "state"."group" = 'DocState'
WHERE pws.record_state <> 4