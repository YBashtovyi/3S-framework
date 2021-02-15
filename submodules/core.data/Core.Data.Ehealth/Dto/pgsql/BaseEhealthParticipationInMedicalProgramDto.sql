select x.id,
    coalesce(x.caption, '') as caption,
    x.medical_service_program_id,
    x.service_catalog_group_id,
    x.service_catalog_service_id,
    x.is_active,
    x.request_allowed,
    coalesce(x.consumer_price, 0) as consumer_price,
    coalesce(x."description", '') as "description",
    x.inserted_at_in_ehealth,
    x.updated_at_in_ehealth,
    x.ehealth_id
from ehe_participation_in_medical_program x
where x.record_state <> 4