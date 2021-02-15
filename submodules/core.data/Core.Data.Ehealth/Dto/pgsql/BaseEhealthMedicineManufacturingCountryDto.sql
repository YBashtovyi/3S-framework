SELECT x.id,
    x.caption,
    x.code,
    x.description
FROM ehp_medicine_manufacturing_country x
WHERE x.record_state <> 4