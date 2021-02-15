Select 
        pers.caption,
		pers.id,
		pers.last_name,
		pers.name,
		pers.middle_name,
		pers.birthday,
		pers.phone,
		pers.email,
		pers.ipn,
        pers.gender,
        engend.caption as gender_caption        
from cmn_person pers
left join enum_record engend on engend.code = pers.gender
where pers.record_state<>4