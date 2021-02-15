SELECT x.id,
    x.caption,
	COALESCE(x."name", '') as "name",
	COALESCE(x.email, '') as email,
	COALESCE(x.type_code, '') as type_code,
	COALESCE(x.external_id, '') as external_id,
	COALESCE(x.location_latitude, 0) as location_latitude,
    COALESCE(x.ehealth_id, uuid_in('00000000-0000-0000-0000-000000000000')) as ehealth_id,
	COALESCE(x.location_longitude, 0) as location_longitude
FROM ehd_division x
WHERE x.record_state <> 4