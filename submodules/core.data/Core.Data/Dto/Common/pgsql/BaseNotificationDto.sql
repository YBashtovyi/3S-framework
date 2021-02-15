select
  x.id,
  coalesce(x.caption, '') as caption,
  coalesce(x.title, '') as title,
  coalesce(x."message", '') as "message",
  coalesce(x."url", '') as "url",
  x.type_id,
  x.state_id,
  x.particular_time
from cmn_notification x
where x.record_state <> 4
