select 
	sch_setting.id,
	coalesce(sch_resource.id, uuid_in('00000000-0000-0000-0000-000000000000')) as resource_id,
	coalesce(sch_resource.entity_id, uuid_in('00000000-0000-0000-0000-000000000000')) as entity_id,
	coalesce(sch_resource.entity_name, '') as entity_name,
	sch_setting.work_date_from,
	sch_setting.work_date_to,
	sch_setting.schedule_repeat_id,
	sch_setting.day1,
	sch_setting.day2,
	sch_setting.day3,
	sch_setting.day4,
	sch_setting.day5,
	sch_setting.day6,
	sch_setting.day7,
	sch_setting.is_full_day,
	sch_setting.work_time_from,
	sch_setting.work_time_to,
	sch_setting.slot_duration,
	sch_setting.break_between_slots,
	sch_setting.organization_id,
	coalesce(sch_setting.caption, '') as caption
from eq_schedule_setting as sch_setting
	left join eq_schedule_resource as sch_resource on
		sch_resource."id" = sch_setting.resource_id
where sch_setting.record_state <> 4 and sch_resource.record_state <> 4
