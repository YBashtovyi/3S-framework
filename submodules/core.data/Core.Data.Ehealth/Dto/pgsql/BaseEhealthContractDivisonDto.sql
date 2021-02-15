SELECT x.id,
    x.caption,
    x.division_id,
    x."entity_id"
FROM ehd_contract_divison x
WHERE x.record_state <> 4