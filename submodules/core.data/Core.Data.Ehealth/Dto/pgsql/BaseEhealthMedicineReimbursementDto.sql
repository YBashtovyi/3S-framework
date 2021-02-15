SELECT x.id,
    x.caption,
    x.reimbursement_program_id,
    x.start_date,
    x.end_date,
    x.activity_status_Id,
    x.wholesale_price,
    x.retail_price,
    x.compensation_amount,
    x.medicine_id,
    x.minimal_realization_amount
FROM ehp_medicine_reimbursement x
WHERE x.record_state <> 4