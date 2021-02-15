SELECT
	photo."id", 
	photo.created_on, 
	photo.project_id, 
	photo.customer_id, 
	photo.general_contractor_id, 
	photo.doc_type, 
	photo.doc_state, 
	photo.reg_date, 
	photo.reg_number, 
	photo.fixation_type, 
	photo.fixation_state,
	photo.responsible_employee_id,
	photo.description
FROM
	prj_photo_report AS photo
WHERE photo.record_state <> 4