select
	x.id,
	coalesce(x.caption, '') as caption,
	coalesce(x.main_entity_name, '') as main_entity_name,
	coalesce(x.code, '') as code,
	coalesce(x.template, '') as template
from
	cmn_printed_form_template as x
where
	x.record_state <> 4
