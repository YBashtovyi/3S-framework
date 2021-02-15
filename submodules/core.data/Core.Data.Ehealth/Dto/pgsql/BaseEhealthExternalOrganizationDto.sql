SELECT x.id,
    coalesce(x.caption, '') as caption,
    coalesce(x."name", '') as "name",
    coalesce(x.short_name, '') as short_name,
    x."type_id",
    coalesce(x.edrpou, '') as edrpou,    
    coalesce(x.email, '') as email,
    x.nhs_verified,
    x.ehealth_id
FROM ehe_external_organization x
WHERE x.record_state <> 4