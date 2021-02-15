select
    sch_resource.id,
	sch_resource.entity_id,
	sch_resource.entity_name,
    sch_resource.organization_id,
	coalesce(sch_resource.caption, '') as caption
from eq_schedule_resource as sch_resource
where
	sch_resource.record_state <> 4