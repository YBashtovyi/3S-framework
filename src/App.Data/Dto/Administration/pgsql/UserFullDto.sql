select
	main_data.employee_id,
	org.id as organization_id,
	org.full_name as organization_caption,
	per.caption as person_full_name,
	per.first_name as person_first_name,
	per.middle_name as person_middle_name,
	per.last_name as person_last_name,
	per.id as person_id,
	au.id as user_id,
	cp."name" as position_name
from
	(select
		emp.id as employee_id,
		emp.person_id
	from
		org_employee as emp
	where emp.record_state <> 4
	group by
		emp.id,
		emp.person_id) as main_data
left join org_unit_staff as ous on main_data.employee_id = ous.employee_id and ous.record_state <> 4
left join org_unit_position oup on ous.org_unit_position_id = oup.id and oup.record_state <> 4
left join cdn_position as cp on oup.position_id = cp.id
left join organization as org on oup.org_unit_id = org.id and org.record_state <> 4
left join cmn_person as per on main_data.person_id = per.id and per.record_state <> 4
left join adm_user as au on main_data.employee_id = au.employee_id and au.record_state <> 4