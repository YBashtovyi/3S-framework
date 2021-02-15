select x.id, 
	coalesce(x.caption, '') as caption,
    x."entity_id",
    coalesce(x."entity_name", '') as "entity_name" ,
    x.related_entity_id,
    coalesce(x.related_entity_name, '') as related_entity_name,
    coalesce(x.relation_type, '') as relation_type
from cmn_entity_relation x
where x.record_state != 4