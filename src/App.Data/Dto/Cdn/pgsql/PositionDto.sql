SELECT
	pos."id", 
	pos.created_on, 
	COALESCE(pos.code, '') as code, 
	COALESCE(pos."name", '') as name, 
	COALESCE(pos.caption, '') as caption
FROM
	cdn_position AS pos