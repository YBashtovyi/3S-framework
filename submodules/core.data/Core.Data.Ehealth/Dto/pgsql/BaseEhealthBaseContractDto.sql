SELECT x.id,
    x.caption,
    COALESCE(x.number, '') as number,
    COALESCE(x.issued_at, CURRENT_TIMESTAMP) as issued_at, 
    COALESCE(x.expires_at, CURRENT_TIMESTAMP) as expires_at,    
    COALESCE(x.entity_id, uuid_in('00000000-0000-0000-0000-000000000000')) as entity_id
FROM ehd_base_contract x
WHERE x.record_state <> 4