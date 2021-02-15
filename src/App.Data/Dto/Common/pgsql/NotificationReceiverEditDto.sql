select x.id,
	coalesce(x.caption, '') as caption,
	x.notification_id,
	x.receiver_id,
	coalesce(oe.caption, '') as receiver_caption,
    x.organization_id
from cmn_notification_receiver x
    join org_employee oe on x.receiver_id = oe.id
where x.record_state <> 4