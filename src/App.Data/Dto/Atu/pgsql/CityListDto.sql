select
	cit.id,
	cit."name",
	cit.record_state,
	cit.created_on 
from
	atu_city as cit
where cit.record_state <> 4

