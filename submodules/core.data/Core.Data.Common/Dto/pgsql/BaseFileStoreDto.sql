select
  cfs.caption,
  cfs.id,
  cfs.file_type,
  cfs.entity_name,
  cfs.entity_id,
  cfs.file_path,
  cfs.file_name,
  cfs.content_type,
  cfs.file_size,
  cfs.description,
  cfs.document_type_id,
  cfs.file_group_id
from cmn_file_store cfs
where cfs.record_state <> 4
