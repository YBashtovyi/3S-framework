SELECT x.id,
    x.caption,
    COALESCE(x.division_id, uuid_in('00000000-0000-0000-0000-000000000000')) as division_id,
    COALESCE(x.employee_id, uuid_in('00000000-0000-0000-0000-000000000000')) as employee_id,
    COALESCE(x.scope, '') as scope,
    COALESCE(x.ehealth_id, uuid_in('00000000-0000-0000-0000-000000000000')) as ehealth_id,
    x.overlimit
FROM ehd_declaration x
WHERE x.record_state <> 4