SELECT x.id, 
    COALESCE(x.caption, '') as caption,
	x.prescription_id,
    COALESCE(x.manufacturer_name, '') as manufacturer_name,
    COALESCE(x.manufacturer_country, '') as manufacturer_country,
	x.ehealth_id,
    COALESCE(x.medication_name, '') as medication_name,
    COALESCE(x.medication_quantity, 0) as medication_quantity
FROM ehp_dispenses_detail x
WHERE x.record_state <> 4