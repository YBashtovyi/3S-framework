SELECT x.id,
    coalesce(x.caption, '') as caption,
    coalesce(x.name, '') as name,
    x."type_id",
    x.ehealth_id
FROM ehe_external_division x
WHERE x.record_state <> 4