select x.id,
    coalesce(x.caption, '') as caption,
    x.group_id,
    x.service_id
from ehe_service_catalog_service_in_group x
where x.record_state <> 4