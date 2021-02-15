SELECT x.id,
    x.caption,
    x.manufacturer_type_id,
    x.edrpou,
    x.manufacturing_country_id,
    x.description,    
    x.is_active
FROM ehp_medicine_manufacturer x
WHERE x.record_state <> 4