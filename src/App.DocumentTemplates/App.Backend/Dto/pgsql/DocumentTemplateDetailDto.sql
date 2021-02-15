select
	t.id,
	t.caption,
	t.entity_name,
	t.parent_id,
	t.order_number,
	t.class_short_code,
	t.code,
	t.description,
	t.owner_id,
	t.template_path,
	coalesce(parent.caption, '') as parent_caption,
	coalesce(o.caption, '') as owner_caption
from
	dtm_template as t
left join dtm_template as parent on
	t.parent_id = parent.id
	and parent.record_state <> 4
left join org_organization as o on
	t.owner_id = o.id
	and o.record_state <> 4
where
	t.record_state <> 4