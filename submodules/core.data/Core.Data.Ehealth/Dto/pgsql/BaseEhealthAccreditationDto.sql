SELECT x.id,
    x.caption,
    COALESCE(x.category_code, '') as category_code,
    COALESCE(x.issued_date, CURRENT_TIMESTAMP) as issued_date, 
    COALESCE(x."expiry_date", CURRENT_TIMESTAMP) as "expiry_date", 
	COALESCE(x.order_no, '') as order_no,
    COALESCE(x.order_date, CURRENT_TIMESTAMP) as order_date
FROM ehd_accreditation x
WHERE x.record_state <> 4