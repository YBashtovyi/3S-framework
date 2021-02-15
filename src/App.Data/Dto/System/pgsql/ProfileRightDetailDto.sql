select
	p_right.id,
	prof.caption || ', ' || righ.caption as caption,
	p_right.profile_id,
	p_right.right_id,
	case
		when prof.is_active = true
		and righ.is_active = true then true
		else false
	end as is_active
from
	sys_profile_right as p_right
inner join sys_profile as prof on
	p_right.profile_id = prof.id
	and prof.record_state <> 4
inner join sys_right as righ on
	p_right.right_id = righ.id
	and righ.record_state <> 4
where
	p_right.record_state <> 4
	and prof.record_state <> 4
	and righ.record_state <> 4
