SELECT x.id,
    COALESCE(x.caption, '') as caption,
    x.code,
    COALESCE(x."name", '') as "name",
    COALESCE(x.name_original, '') as name_original
FROM ehp_international_nonproprietary_name x
WHERE x.record_state <> 4