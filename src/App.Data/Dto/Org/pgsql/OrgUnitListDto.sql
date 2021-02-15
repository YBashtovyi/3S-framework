SELECT
	org."id",
	org.created_on,
	COALESCE ( org."name", '' ) AS "name",
	COALESCE ( org.code, '' ) AS code,
	COALESCE ( org.derived_entity, '' ) AS derived_entity 
FROM
	org_unit AS org 
WHERE
	org.record_state <> 4