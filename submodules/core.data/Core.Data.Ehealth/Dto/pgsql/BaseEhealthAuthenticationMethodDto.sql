select  x.id,
    x.caption,
	coalesce(x.type_code, '') as type_code,
	coalesce(x.phone_number, '') as phone_number,
	x.entity_id,
    x.third_person_ehealth_id,
    coalesce(x.alias, '') as alias
from ehd_authentication_method x
where x.record_state <> 4