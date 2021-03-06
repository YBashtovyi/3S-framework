select
  x.id,
  x.caption,  
  coalesce( x.employee_id, uuid_in('00000000-0000-0000-0000-000000000000')) as employee_id,
  coalesce( x.speciality_id, uuid_in('00000000-0000-0000-0000-000000000000')) as  speciality_id, 
  x.is_main_speciality
from cdn_employee_speciality x
where x.record_state <> 4   