SELECT
	pwsst."id", 
	pwsst.prj_work_schedule_id,
	pwsst.prj_work_schedule_stage_id, 
	pwsst.work_sub_type_id, 
	pwsst.measurement_unit, 
	pwsst.amount, 
	pwsst.target, 
	pwsst.begin_date, 
	pwsst.end_date
FROM
	prj_work_schedule_sub_type AS pwsst
WHERE pwsst.record_state <> 4