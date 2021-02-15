select
	id,
	caption,
	entity_name,
	parent_id,
	order_number,
	class_short_code,
	code,
	description,
	owner_id,
    template_path
from
	dtm_template
where
	record_state <> 4
