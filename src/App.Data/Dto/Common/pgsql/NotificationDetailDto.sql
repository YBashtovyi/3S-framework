select
  x.id,
  x.created_on as created_at,
  coalesce(x.caption, '') as caption,
  coalesce(x.title, '') as title,
  coalesce(x."message", '') as "message",
  coalesce(x."url", '') as "url",
  x.type_id,
  x.state_id,
  x.particular_time,
  x.one_signal_id,
  coalesce(x.error, '') as error,
  coalesce(notification_type.caption, '') as type_caption,
  coalesce(notification_state.code, '') as state_code,
  coalesce(notification_state.caption, '') as state_caption,
  x.created_by as author_id,
  coalesce(author.caption, '') as author_caption,
  x.create_in_one_signal_date,
  x.organization_id
from cmn_notification x
	join cmn_enum_record notification_type on notification_type.id = x.type_id
	join cmn_enum_record notification_state on notification_state.id = x.state_id
	left join org_employee author on author.id = x.created_by and author.record_state <> 4
where x.record_state <> 4
