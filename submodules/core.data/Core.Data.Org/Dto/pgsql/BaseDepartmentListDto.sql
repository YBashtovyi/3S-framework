select
 x.caption,
 x.id,
 x.full_name
from
 org_department as x
where ( x.record_state <> 4 )