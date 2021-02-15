using System;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Prj
{
    [MainEntity(nameof(Project))]
    public class ProjectConstructionObjectListDto: CoreDto, IPagingCounted
    {
        public DateTime CreatedOn { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Code { get; set; }

        [CaseFilter]
        public string ProjectStatus { get; set; }

        [CaseFilter]
        public Guid RegionId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter(CaseFilterOperation.ValueRange)]
        public DateTime? DateBegin { get; set; }

        [CaseFilter(CaseFilterOperation.ValueRange)]
        public DateTime? DateEnd { get; set; }

        [CaseFilter]
        public Guid ConstructionObjectId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegionName { get; set; }

        public string ProjectStatusName { get; set; }

        [CaseFilter]
        public string ProjectImplementationState { get; set; }
        public string ProjectImplementationStateName { get; set; }

        public int TotalRecordCount { get; set; }
    }
}
