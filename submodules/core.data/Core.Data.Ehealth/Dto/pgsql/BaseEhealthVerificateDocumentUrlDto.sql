SELECT x.id,
    COALESCE(x.caption, '') as caption,
    COALESCE(x."type", '') as "type",
    COALESCE(x."url", '') as "url",
    x."entity_id",
    x.is_uploaded
FROM ehd_verificate_document_url x
WHERE x.record_state <> 4