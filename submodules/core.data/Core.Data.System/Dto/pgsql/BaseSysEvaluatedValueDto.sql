select
	x.id,    
	coalesce(x.caption, '') as caption,
    coalesce(x."entity_name", '') as "entity_name",
    coalesce(x."entity_id", uuid_in('00000000-0000-0000-0000-000000000000')) as "entity_id",
    coalesce(x.value_name, '') as value_name,
    coalesce(x.value_type, '') as value_type,
    coalesce(x."value", '') as "value"
from sys_evaluated_value x
where x.record_state <> 4