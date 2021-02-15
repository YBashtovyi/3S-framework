select
    a.caption,
    a.caption as name,
	a.id,
    coalesce(a.type_enum, '') as type_enum,	
    x1.caption as type,	
	a.code,
	a.region_id,
	r.caption as region_name
from
	atu_city as a
	inner join atu_region as r on a.region_id = r.id and r.record_state<>4
    left join cmn_enum_record as x1 on lower(x1.code) = lower(a.type_enum) and x1.enum_type = 'CityType'::text
where a.record_state <> 4