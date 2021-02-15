select 
pc.id,
pc.enabled,
pc.description,
pc.caption,
pc.reg_date,
pc.reg_number,
p.last_name || ' ' || p.name || ' ' || p.middle_name as person_full_name,
p.birthday,
p.email,
p.phone,
p.gender,
(select caption from cmn_enum_record  where  enum_type = 'Gender'  and code = p.gender) as gender_caption
from cmn_person p
	join mis_patient_card pc on p.id = pc.person_id and pc.record_state <> 4
where p.record_state <> 4