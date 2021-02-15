select
	u_prof.id,
	pers.caption || ', ' || prof.caption as caption,
	u_prof.user_id,
	u_prof.profile_id,
	oe.organization_id,
	coalesce(oe.department_id, uuid_in('00000000-0000-0000-0000-000000000000')) as department_id
from
	sys_user_profile as u_prof
inner join org_employee as oe on
	u_prof.user_id = oe.id
	inner join cmn_person as pers on
	    oe.person_id = pers.id
inner join sys_profile as prof on
    u_prof.profile_id = prof.id
where
	u_prof.record_state <> 4
	and prof.record_state <> 4
	and oe.record_state <> 4
