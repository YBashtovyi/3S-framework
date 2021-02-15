SELECT x.id,
    coalesce(x.caption, '') as caption,
    x.is_active,
    x.hidden,
    x.ehealth_id
FROM ehe_medical_service_program x
WHERE x.record_state <> 4