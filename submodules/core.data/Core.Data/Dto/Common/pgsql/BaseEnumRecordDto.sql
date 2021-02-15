SELECT 
    x.id,
    x.record_state,
    x.name,
    x.group,
    x.code,
    x.value
FROM cmn_enum_record as x where record_state != 4