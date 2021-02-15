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
    [MainEntity(nameof(ProjectWorkScheduleStage))]
    public class ProjectWorkScheduleStageListDto: CoreDto, IPagingCounted
    {
        [CaseFilter]
        public Guid PrjWorkScheduleId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string StageNumber { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string StageName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime BeginDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime EndDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal Cost { get; set; }


        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ProjectWorkScheduleStage))]
    public class ProjectWorkScheduleStageEditDto : CoreDto
    {
        public Guid PrjWorkScheduleId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string StageNumber { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string StageName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime BeginDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime EndDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal Cost { get; set; }
    }

    [MainEntity(nameof(ProjectWorkScheduleStage))]
    public class ProjectWorkScheduleStageDetailsDto : CoreDto
    {
        public Guid PrjWorkScheduleId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string StageNumber { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string StageName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime BeginDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime EndDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal Cost { get; set; }
    }
}
