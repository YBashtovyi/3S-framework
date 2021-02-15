select
	id,
	record_state,
	caption,
	modified_by,
	modified_on,
	created_by,
	created_on,
	parent_id,
	order_number,
	class_short_code,
	code,
	description,
	owner_id
from
	dtm_template
where
	record_state <> 4
