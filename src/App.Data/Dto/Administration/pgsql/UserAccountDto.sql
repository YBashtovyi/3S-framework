SELECT
	aua."id", 
	aua.user_id, 
	aua.auth_provider, 
	aua.account_id
FROM
	adm_user_account AS aua
WHERE aua.record_state <> 4