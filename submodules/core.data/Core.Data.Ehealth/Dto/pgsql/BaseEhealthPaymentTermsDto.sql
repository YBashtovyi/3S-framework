SELECT x.id,
    x.caption,
    x.code,
    x.is_active
FROM ehp_payment_terms x
WHERE x.record_state <> 4