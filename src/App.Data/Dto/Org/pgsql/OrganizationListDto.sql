SELECT
	org."id",
	org.created_on,
	COALESCE ( org."name", '' ) AS "name",
	COALESCE ( org.code, '' ) AS code,
	COALESCE ( org.edrpou, '' ) AS edrpou,
	COALESCE ( org.org_state, '' ) AS org_state,
	COALESCE ( org_state.name, '' ) AS org_state_name,
	COALESCE ( org.organization_category, '' ) AS organization_category,
	COALESCE ( org_category.name, '' ) AS organization_category_name
FROM
	organization AS org
	LEFT JOIN cmn_enum_record AS org_state ON org.org_state = org_state.code AND org_state."group" = 'OrgState' 
	LEFT JOIN cmn_enum_record AS org_category ON org.organization_category = org_category.code AND org_category."group" = 'OrganizationCategory' 
WHERE
	org.record_state <> 4