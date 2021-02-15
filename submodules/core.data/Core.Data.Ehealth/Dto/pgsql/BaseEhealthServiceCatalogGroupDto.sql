select x.id,
    coalesce(x.caption, '') as caption,
    coalesce(x.code, '') as code,
    coalesce(x."name", '') as "name",
    x.is_active,
    x.request_allowed,
    x.inserted_at_in_ehealth,
    x.updated_at_in_ehealth,
    x.parent_id,
    x.ehealth_id
from ehe_service_catalog_group x
where x.record_state <> 4