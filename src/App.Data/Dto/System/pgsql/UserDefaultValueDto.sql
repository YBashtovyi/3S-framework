select
	u_defaults.id,
	u_defaults.entity_name,
	u_defaults.user_id,
	u_defaults.value_id
from
	sys_user_default_value as u_defaults
where
	u_defaults.record_state <> 4