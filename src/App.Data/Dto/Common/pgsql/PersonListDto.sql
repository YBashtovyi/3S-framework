SELECT
	p."id", 
    p.created_on,
	p.caption, 
	p.first_name, 
	p.middle_name, 
	p.last_name, 
	p.birthday, 
	p.gender, 
	COALESCE(gender.name, '') as gender_name,
	p.identity_document, 
	COALESCE(idoc.name, '') as identity_document_name,
	p.no_tax_number, 
	p.tax_number
FROM
	cmn_person as p
LEFT JOIN cmn_enum_record as gender ON p.gender = gender.code and gender."group" = 'Gender'
LEFT JOIN cmn_enum_record as idoc ON p.identity_document = idoc.code and idoc."group" = 'IdentityDocument'
WHERE
	p.record_state <> 4