select 
    pr.caption,
	pr.id,
	pr.name,
	pr.code,
	ep.entity_id,
	ep.value,
    pr.kind_enum,
    pr.prop_type_enum,
    pr.cotype_enum,
    pr.sort_order,
    pr.group,
    ep.ex_property_id,
    CASE 
        when pr.prop_type_enum='SELECTLIST' then 
        	(select name from enum_record  where  enum_type = pr.kind_enum  and code = ep.value)  
        else null
    END as value_enum
from ex_property as pr
	left join entity_ex_property as ep on pr.id=ep.ex_property_id and ep.record_state<>4
where pr.record_state<>4