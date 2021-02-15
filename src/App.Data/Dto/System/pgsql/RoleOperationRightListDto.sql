select
	r_right.id,
	rol.caption || ', ' || op_right.caption as caption,
	r_right.role_id,
	r_right.operation_right_id
from
	sys_role_operation_right as r_right
inner join sys_role as rol
    on r_right.role_id = rol.id
inner join sys_operation_right as op_right
    on r_right.operation_right_id = op_right.id
where
	r_right.record_state <> 4
	and rol.record_state <> 4
	and op_right.record_state <> 4