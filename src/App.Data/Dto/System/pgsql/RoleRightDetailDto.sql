select
	r_right.id,
	rol.caption || ', ' || righ.caption as caption,
	r_right.role_id,
	r_right.right_id,
	case
		when rol.is_active = true
		and righ.is_active = true then true
		else false
	end as is_active
from
	sys_role_right as r_right
inner join sys_role as rol on
	r_right.role_id = rol.id
inner join sys_right as righ on
	r_right.right_id = righ.id	
where
	r_right.record_state <> 4
	and rol.record_state <> 4
	and righ.record_state <> 4
