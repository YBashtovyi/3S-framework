select 
	id,
	record_state,
	modified_by,
	modified_on,
	created_by,
	created_on,
	entity_id,
	entity_name,
	owner,
	action,
	processed,
	is_error,
	error_message
from cmn_pending_change
where record_state <> 4
