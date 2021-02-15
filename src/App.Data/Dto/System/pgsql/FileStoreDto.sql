select
  cfs.id,
  cfs.file_type,
  cfs.entity_name,
  cfs.entity_id,
  cfs.file_path,
  cfs.file_name,
  cfs.description,
  cfs.created_on,
	cer."name" as type_of_attached_file_name
from cmn_file_store cfs
JOIN cmn_enum_record as cer on cfs.type_of_attached_file = cer.code and cer."group" = 'AttachedFile'
where cfs.record_state <> 4
