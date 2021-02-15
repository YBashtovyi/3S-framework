SELECT x.id,
    x.caption,
    x.medicine_manufacturer_id,
    x.medicine_active_substance_id,
    x.amount_in_pack,
    x.medicine_unit_measures_id,
    x.recommended_daily_dose,
    x.registration_sertificate_number,
    x.activity_status_id,
    x.medicine_release_form_id
FROM ehp_medicines x
WHERE x.record_state <> 4