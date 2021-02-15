SELECT
	photo."id", 
	photo.created_on, 
	photo.project_id, 
	photo.customer_id, 
	customer."name" AS customer_name,
	photo.general_contractor_id, 
	generalContractor."name" AS general_contractor_name,
	photo.doc_type, 
	docType."name" AS doc_type_name,
	photo.doc_state, 
	docState."name" AS doc_state_name,
	photo.reg_date, 
	photo.reg_number, 
	photo.fixation_type, 
	fixType."name" AS fixation_type_name, 
	photo.fixation_state,
	fixState."name" AS fixation_state_name
FROM
	prj_photo_report AS photo
INNER JOIN cmn_enum_record AS docType ON photo.doc_type = docType.code AND docType."group" = 'DocType'
INNER JOIN cmn_enum_record AS docState ON photo.doc_state = docState.code AND docState."group" = 'DocState'
INNER JOIN cmn_enum_record AS fixType ON photo.fixation_type = fixType.code AND fixType."group" = 'FixationType'
INNER JOIN cmn_enum_record AS fixState ON photo.fixation_state = fixState.code AND fixState."group" = 'FixationState'
INNER JOIN organization AS customer ON photo.customer_id = customer.id
INNER JOIN organization AS generalContractor ON photo.general_contractor_id = generalContractor.id
WHERE photo.record_state <> 4