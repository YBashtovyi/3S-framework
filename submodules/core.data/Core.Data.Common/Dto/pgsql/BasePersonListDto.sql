Select 
        caption,
		id,
		last_name || ' ' || name || ' ' || middle_name as full_name
from cmn_person
where record_state<>4