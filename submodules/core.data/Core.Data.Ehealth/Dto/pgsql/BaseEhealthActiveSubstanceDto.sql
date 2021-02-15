SELECT x.id,
    x.caption,
    x.international_nonproprietary_name_id,
    x.medicine_release_form_id,
    x.dosage,
    x.medicine_action_unit_measures_id,
    x.is_active,
    x.anatomical_therapeutic_chemical_code
FROM ehp_active_substance x
WHERE x.record_state <> 4