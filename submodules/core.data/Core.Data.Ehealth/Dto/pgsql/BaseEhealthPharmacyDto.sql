SELECT x.id,
    x.caption,
    x.edrpou,
    x.description,    
    x.is_active
FROM ehp_pharmacy x
WHERE x.record_state <> 4