select
    a.caption,
	a.id,
	a.code,
	a.parent_id,
	par.caption as parent_caption,
	a.country_id as country_id,
	c.caption as country_caption
from atu_region as a left join atu_country as c on a.country_id=c.id and c.record_state<>4
						left join atu_region as par on a.parent_id=par.id and par.record_state<>4
where a.record_state <> 4