SELECT x.id,
    x.caption,
    x.code
FROM ehp_medicine_taking_method x
WHERE x.record_state <> 4