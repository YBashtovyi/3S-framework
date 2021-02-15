select
	rl_right.id,
	rl_right.caption,
	rl_right.profile_id,
	rl_right.entity_name,
	rl_right.main_entity_name,
	rl_right.access_type
from
	sys_row_level_right as rl_right
where
	rl_right.record_state <> 4