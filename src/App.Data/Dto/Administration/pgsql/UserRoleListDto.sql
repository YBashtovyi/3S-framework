SELECT
	"user"."id" AS user_id,
	json_array_elements_text ( "user".roles ) :: uuid AS role_id
FROM
	adm_user AS "user"
WHERE
	roles IS NOT NULL AND "user".record_state <> 4