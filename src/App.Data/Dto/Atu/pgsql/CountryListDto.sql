select
	cnt.id, 
	cnt.code,
	cnt."name",
	cnt.full_name,
	cnt."comment",
	cnt.created_on    
from 
	atu_country cnt
where (1=1)
and	cnt.record_state <> 4

