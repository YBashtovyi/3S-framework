select
  x.id,
  x.parent_id,
  x.caption
from cdn_speciality x
where x.record_state <> 4