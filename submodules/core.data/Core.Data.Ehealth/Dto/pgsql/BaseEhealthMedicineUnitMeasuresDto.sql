SELECT x.id,
    x.caption,
    x.code
FROM ehp_medicine_unit_measures x
WHERE x.record_state <> 4