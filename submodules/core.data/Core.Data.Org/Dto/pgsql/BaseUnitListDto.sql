select
    d.caption,
	d.id,
	d.name,
	d.code
from
	org_unit as d
where ( d.record_state <> 4 )