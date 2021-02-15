select
	p_role.id,
	prof.caption || ', ' || rol.caption as caption,
	p_role.profile_id,
	p_role.role_id,
	case
		when prof.is_active = true
		and rol.is_active = true then true
		else false
	end as is_active
from
	sys_profile_role as p_role
left join sys_profile as prof on
	p_role.profile_id = prof.id
	and prof.record_state <> 4
left join sys_role as rol on
	p_role.role_id = rol.id
	and rol.record_state <> 4
where
	p_role.record_state <> 4
	and prof.record_state <> 4 
	and rol.record_state <> 4
