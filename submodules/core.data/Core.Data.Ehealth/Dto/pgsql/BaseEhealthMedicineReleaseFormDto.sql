SELECT x.id,
    x.caption,
    x.code,
    x.parent_id
FROM ehp_medicine_release_form x
WHERE x.record_state <> 4