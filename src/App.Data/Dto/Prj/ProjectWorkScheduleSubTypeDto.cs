using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Prj
{
    [MainEntity(nameof(ProjectWorkScheduleSubType))]
    public class ProjectWorkScheduleSubTypeListDto : CoreDto, IPagingCounted
    {
        [CaseFilter]
        public Guid PrjWorkScheduleId { get; set; }

        [CaseFilter]
        public Guid PrjWorkScheduleStageId { get; set; }

        public string PrjWorkScheduleStageName { get; set; }

        [CaseFilter]
        public Guid WorkSubTypeId { get; set; }

        public string WorkSubTypeName { get; set; }

        public string WorkSubTypeCode { get; set; }

        [CaseFilter]
        public string MeasurementUnit { get; set; }

        public string MeasurementUnitValue { get; set; }

        public float Amount { get; set; }

        public float Target { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime BeginDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime EndDate { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ProjectWorkScheduleSubType))]
    public class ProjectWorkScheduleSubTypeEditDto : CoreDto
    {
        [CaseFilter]
        public Guid PrjWorkScheduleId { get; set; }

        [CaseFilter]
        public Guid PrjWorkScheduleStageId { get; set; }

        [CaseFilter]
        public Guid WorkSubTypeId { get; set; }

        [CaseFilter]
        public string MeasurementUnit { get; set; }

        public float Amount { get; set; }

        public float Target { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime BeginDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime EndDate { get; set; }
    }

    [MainEntity(nameof(ProjectWorkScheduleSubType))]
    public class ProjectWorkScheduleSubTypeDetailsDto : CoreDto
    {
        [CaseFilter]
        public Guid PrjWorkScheduleStageId { get; set; }

        public string PrjWorkScheduleStageName { get; set; }

        [CaseFilter]
        public Guid WorkSubTypeId { get; set; }

        public string WorkSubTypeName { get; set; }

        public string WorkSubTypeCode { get; set; }

        [CaseFilter]
        public string MeasurementUnit { get; set; }

        public string MeasurementUnitName { get; set; }

        public float Amount { get; set; }

        public float Target { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime BeginDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime EndDate { get; set; }
    }
}
