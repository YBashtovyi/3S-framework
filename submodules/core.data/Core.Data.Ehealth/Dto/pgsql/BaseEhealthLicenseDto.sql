SELECT x.id, 
    caption,
	COALESCE(x.type_code, '') as type_code,
	COALESCE(x.license_number, '') as license_number,
	COALESCE(x.issued_by, '') as issued_by,
	COALESCE(x.issued_date, CURRENT_TIMESTAMP) as issued_date, 
	COALESCE(x.expiry_date, CURRENT_TIMESTAMP) as expiry_date, 
	COALESCE(x.active_from_date, CURRENT_TIMESTAMP) as active_from_datep, 
	COALESCE(x.what_licensed, '') as what_licensed,
	COALESCE(x.order_no, '') as order_no,
	x.entity_id
FROM ehd_license x
WHERE record_state <> 4