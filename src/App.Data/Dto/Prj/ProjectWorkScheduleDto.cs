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
    [MainEntity(nameof(ProjectWorkSchedule))]
    public class ProjectWorkScheduleListDto: CoreDto, IPagingCounted
    {
        [CaseFilter]
        public Guid ProjectId { get; set; }

        [CaseFilter]
        public string DocType { get; set; }
        public string DocTypeName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime RegDate { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegNumber { get; set; }

        [CaseFilter]
        public string DocState { get; set; }
        public string DocStateName { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ProjectWorkSchedule))]
    public class ProjectWorkScheduleEditDto : CoreDto
    {
        [CaseFilter]
        public Guid ProjectId { get; set; }

        [CaseFilter]
        public string DocType { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime RegDate { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegNumber { get; set; }

        [CaseFilter]
        public string DocState { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Description { get; set; }

        public Guid? ParentId { get; set; }
    }

    [MainEntity(nameof(ProjectWorkSchedule))]
    public class ProjectWorkScheduleDetailsDto : CoreDto
    {
        [CaseFilter]
        public Guid ProjectId { get; set; }

        [CaseFilter]
        public string DocType { get; set; }

        public string DocTypeName { get; set; }

        [CaseFilter]
        public Guid? ParentId { get; set; }

        public DateTime? ParentRegDate { get; set; }

        public string ParentRegNumber { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime RegDate { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegNumber { get; set; }

        [CaseFilter]
        public string DocState { get; set; }

        public string DocStateName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Description { get; set; }
    }
}
