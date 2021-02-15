select
	f_right.id,
	f_right.caption,
	f_right.right_id,
	f_right.field_name,
	f_right.access_level
from
	sys_field_right as f_right
where
	f_right.record_state <> 4