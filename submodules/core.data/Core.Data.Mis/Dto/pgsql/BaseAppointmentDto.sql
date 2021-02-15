select
	ap.id,
	ap.caption,
	ap.start_date,
	ap.end_date,
	ap.state_id,
	ap.interaction_type_id,
	ap.description,
	ap.employee_id,
	ap.patient_card_id,
	ap.organization_id
from
	mis_appointment as ap
where
	ap.record_state <> 4
