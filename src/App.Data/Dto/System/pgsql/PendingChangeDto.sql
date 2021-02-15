select 
	id,
	entity_id,
	entity_name,
	owner,
	action,
	processed,
	modified_on,
    created_on,
	is_error,
	error_message
from cmn_pending_change
where record_state <> 4
