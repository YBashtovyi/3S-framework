SELECT x.id,
    x.caption,
	COALESCE(x.edrpou, '') as edrpou,
	COALESCE(x.type_code, '') as type_code,
	COALESCE(x.email, '') as email,
	COALESCE(x.website, '') as website,
	COALESCE(x.receiver_funds_code, '') as receiver_funds_code, 
	COALESCE(x.beneficiary, '') as beneficiary,
	COALESCE(x.security_redirect_uri, '') as security_redirect_uri,
	COALESCE(x.public_offer_consent_text, '') as public_offer_consent_text,
    COALESCE(x.ehealth_id, uuid_in('00000000-0000-0000-0000-000000000000')) as ehealth_id,
	x.public_offer_consent
FROM ehd_organization x
WHERE x.record_state <> 4