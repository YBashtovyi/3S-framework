select
	prof.id,
	prof.caption,
	prof.is_active
from
	sys_profile as prof
where
	prof.record_state <> 4
