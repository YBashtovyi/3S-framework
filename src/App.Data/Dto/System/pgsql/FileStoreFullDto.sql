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
  cfs.file_group_id,
  cfs.created_on,
  cfs.file_store_destination_type,
  coalesce(dt.caption, '') as document_type_caption,
  coalesce(fg.caption, '') as file_group_caption
from cmn_file_store cfs
left join cmn_enum_record as dt on
        cfs.document_type_id = dt.id
left join cmn_enum_record as fg on
        cfs.file_group_id = fg.id
where cfs.record_state <> 4
