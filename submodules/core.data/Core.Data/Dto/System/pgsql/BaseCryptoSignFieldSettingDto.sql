select
	setting.id,
	setting.caption,
	setting.created_on as date_created,
	setting.entity_name,
	setting.field_name,
	setting.sign_field_name
from
	sys_crypto_sign_field_setting as setting
where setting.record_state <> 4