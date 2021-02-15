SELECT x.id,
    x.caption,
    COALESCE(x.bank_name, '') as bank_name,
    COALESCE(x.mfo, '') as mfo,
    COALESCE(x.payer_account, '') as payer_account,
    COALESCE(x.entity_id, uuid_in('00000000-0000-0000-0000-000000000000')) as entity_id
FROM ehd_payment_detail x
WHERE x.record_state <> 4