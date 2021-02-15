select
	ceepv.id,
	coalesce(ceepv.caption, '') as caption,
	ceepv.entity_id,
	ceepv.property_id,
	coalesce(ceepv.value, '') as value,
	cep.property_type_id,
	cep.code as property_name,
	cer.code as property_type_name
from cmn_entity_extended_property_value ceepv
	left join cmn_extended_property cep on ceepv.property_id = cep.id
	left join cmn_enum_record cer on cep.property_type_id = cer.id
where cer.enum_type = 'ExPropertyType'