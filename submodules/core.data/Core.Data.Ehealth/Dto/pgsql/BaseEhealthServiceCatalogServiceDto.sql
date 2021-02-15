select x.id,
    coalesce(x.caption, '') as caption,
    coalesce(x.code, '') as code,
    coalesce(x."name", '') as "name", 
    x.medical_referral_category_id,
    x.is_active,
    x.is_composition,
    x.request_allowed,
    x.inserted_at_in_ehealth,
    x.updated_at_in_ehealth,
    x.ehealth_id
from ehe_service_catalog_service x
where x.record_state <> 4