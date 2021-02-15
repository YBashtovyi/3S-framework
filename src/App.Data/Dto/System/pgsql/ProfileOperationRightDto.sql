select
	p_right.id,
	prof.caption || ', ' || op_right.caption as caption,
	p_right.profile_id,
	p_right.operation_right_id
from
	sys_profile_operation_right as p_right
inner join sys_operation_right as op_right
	on p_right.operation_right_id = op_right.id
inner join sys_profile as prof
	on p_right.profile_id = prof.id
where
	p_right.record_state <> 4 
	and op_right.record_state <> 4
	and prof.record_state <> 4
