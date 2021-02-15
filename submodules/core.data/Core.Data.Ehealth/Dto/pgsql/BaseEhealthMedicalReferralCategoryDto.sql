select x.id,
    coalesce(x.caption, '') as caption,
    coalesce(x.code, '') as code,
    coalesce(x."name", '') as "name", 
    coalesce(x.ehealth_code, '') as ehealth_code, 
    x.is_used_in_ehealth
from ehe_medical_referral_category x
where x.record_state <> 4