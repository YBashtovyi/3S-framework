select
	rlr.id,
	rlr.caption,
	rlr.entity_name,
	rlr.is_active
from
	sys_application_row_level_right as rlr
where
	rlr.record_state <> 4
