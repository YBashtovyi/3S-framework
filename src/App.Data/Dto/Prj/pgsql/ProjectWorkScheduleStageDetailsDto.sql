SELECT
	pwss."id", 
	pwss.created_on, 
	pwss.prj_work_schedule_id, 
	pwss.stage_number, 
	pwss.stage_name, 
	pwss.begin_date, 
	pwss.end_date, 
	pwss."cost"
FROM
	prj_work_schedule_stage AS pwss
WHERE pwss.record_state <> 4