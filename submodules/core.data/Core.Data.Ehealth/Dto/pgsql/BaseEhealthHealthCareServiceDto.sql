SELECT x.id,
	coalesce(x.caption, '') as caption,
	x.licensed_unit_id,
    x.speciality_type_id,
    x.providing_condition_id,
    COALESCE(x.comment, '') as comment,
    COALESCE(x.ehealth_id, uuid_in('00000000-0000-0000-0000-000000000000')) as ehealth_id,
	x.is_active,
    x.is_synced_with_ehealth
FROM ehd_health_care_service x
WHERE x.record_state <> 4