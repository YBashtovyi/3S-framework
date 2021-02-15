select
	righ.id,
	righ.caption,
	righ.operation_name,
	righ.access_level,
	righ.is_active
from
	sys_operation_right as righ
where
	righ.record_state <> 4
