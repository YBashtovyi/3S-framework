select
    a.caption,
	a.id,
	a.address_type_id,
	a.post_index,
	a.building,
	a.street_id,
	s.city_id,
	case when a.address_type <> '' then a.address_type || ': ' else '' end ||
    case when a.post_index <> '' then a.post_index || ', ' else '' end ||
    case when c.caption <> '' then c.caption || '  ' else '' end ||
    case when s.caption <> '' then s.caption || ',  ' else '' end ||
    case when a.building <> '' then a.building else '' end
		as full_address
from atu_subject_address a
inner join atu_street s on s.id = a.street_id and s.record_state<>4
inner join atu_city c on c.id = s.city_id and c.record_state<>4
inner join cmn_enum_record as address_type on a.address_type_id=address_type.id
where ( a.record_state <> 4 )