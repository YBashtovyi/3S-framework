SELECT
	pwsst."id", 
	pwsst.prj_work_schedule_stage_id, 
	pwss.stage_name AS prj_work_schedule_stage_name,
	pwsst.work_sub_type_id, 
	cwst.code AS work_sub_type_code,
	cwst."name" AS work_sub_type_name,
	pwsst.measurement_unit, 
	cer."name" AS measurement_unit_name,
	pwsst.amount, 
	pwsst.target, 
	pwsst.begin_date, 
	pwsst.end_date
FROM
	prj_work_schedule_sub_type AS pwsst
INNER JOIN cmn_enum_record AS cer ON pwsst.measurement_unit = cer.code AND cer."group" = 'MeasurementUnit'
INNER JOIN cdn_work_sub_type AS cwst ON pwsst.work_sub_type_id = cwst.id
INNER JOIN prj_work_schedule_stage AS pwss ON pwsst.prj_work_schedule_stage_id = pwss.id
WHERE pwsst.record_state <> 4