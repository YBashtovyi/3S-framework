﻿Select
e.id,
e.caption,
pc.id as patient_card_id,
coalesce(doc.organization_id, uuid_in('00000000-0000-0000-0000-000000000000')) as organization_id,
e.event_state
from mis_episode e
inner join mis_patient_card pc on e.patient_card_id = pc.id and pc.record_state<> 4
inner join org_employee as doc on  e.doctor_id = doc.id and doc.record_state<>4
where e.record_state<>4