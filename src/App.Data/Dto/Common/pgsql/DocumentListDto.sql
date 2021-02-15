SELECT
	doc."id", 
	doc.created_on, 
	doc.derived_entity, 
	doc.reg_number, 
	doc.reg_date, 
	doc.doc_type, 
	COALESCE(docType.name, '') AS doc_type_name,
	doc.doc_state, 
	COALESCE(docState.name, '') AS doc_state_name,
	doc.description, 
	doc.parent_id
FROM
	cmn_document AS doc
INNER JOIN cmn_enum_record AS docType ON doc.doc_type = docType.code AND docType."group" = 'DocType'
INNER JOIN cmn_enum_record AS docState ON doc.doc_state = docState.code AND docState."group" = 'DocState'
WHERE doc.record_state <> 4