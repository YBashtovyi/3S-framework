select 
    id, 
    caption,
    main_entity_id,
    coalesce(main_entity_name, '') as main_entity_name,
    related_entity_id,
    coalesce(related_entity_name, '') as related_entity_name,
    coalesce(relation_type, '') as relation_type
from ehe_entity_relation 
where record_state != 4