SELECT
	org."id",
	COALESCE ( org."name", '' ) AS NAME,
	COALESCE ( org.full_name, '' ) AS full_name,
	COALESCE ( org.code, '' ) AS code,
	COALESCE ( org.description, '' ) AS description,
	COALESCE ( org.org_type, '' ) AS org_type,
	COALESCE ( org.edrpou, '' ) AS edrpou,
    COALESCE ( org.org_state, '' ) AS org_state,
	COALESCE ( org.organization_category, '' ) AS organization_category
FROM
	organization AS org
WHERE
	org.record_state <> 4