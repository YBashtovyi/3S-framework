select
	righ.id,
	righ.caption,
	righ.entity_name,
	righ.entity_access_level,
	righ.is_active
from
	sys_right as righ
where
	righ.record_state <> 4
