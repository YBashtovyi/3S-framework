select
	x.id,
	x.org_unit_id,
	pos.name as caption
from org_unit_position   as x
  inner join cdn_position as pos on x.position_id = pos.id
where ( x.record_state <> 4 )