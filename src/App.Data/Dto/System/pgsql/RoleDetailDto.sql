select
	rol.id,
	rol.caption,
	rol.is_active
from
	sys_role as rol
where
	rol.record_state <> 4
