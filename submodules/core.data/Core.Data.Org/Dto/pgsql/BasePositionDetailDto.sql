select 
	p.id,
	p.name,
    p.caption
from cdn_position as p
where p.record_state<>4