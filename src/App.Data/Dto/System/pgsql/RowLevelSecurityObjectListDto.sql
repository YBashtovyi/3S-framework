select
	sec_obj.id,
	sec_obj.row_level_right_id,
	sec_obj.entity_id
from
	sys_row_level_security_object as sec_obj
where
	sec_obj.record_state <> 4
