SELECT x.id,
    x.caption,
    COALESCE(x.contractor_owner_id, uuid_in('00000000-0000-0000-0000-000000000000')) as contractor_owner_id,
    COALESCE(x.contractor_base, '') as contractor_base,
    COALESCE(x."start_date", CURRENT_TIMESTAMP) as "start_date", 
    COALESCE(x."end_date", CURRENT_TIMESTAMP) as "end_date", 
    COALESCE(x.id_form, '') as id_form,
    COALESCE(x.contract_number, '') as contract_number,
    COALESCE(x.statute_md5, '') as statute_md5,
    COALESCE(x.additional_document_md5, '') as additional_document_md5,
    COALESCE(x.consent_text, '') as consent_text,
    x.contractor_rmsp_amount,
    x.external_contractor_flag,
    COALESCE(x.previous_request_id, uuid_in('00000000-0000-0000-0000-000000000000')) as previous_request_id,
    x.ehealth_id
FROM ehd_contract x
WHERE x.record_state <> 4