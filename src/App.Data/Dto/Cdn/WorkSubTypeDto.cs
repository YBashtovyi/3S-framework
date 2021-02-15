using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models.cdn;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Cdn
{
    public class WorkSubTypeDto : CoreDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string Code { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter]
        public string MeasurementUnit { get; set; }

        [CaseFilter]
        public string ClassifierType { get; set; }

        [CaseFilter]
        public bool IsActive { get; set; }

        [CaseFilter]
        public Guid? ParentId { get; set; }
    }

    [MainEntity(nameof(WorkSubType))]
    public class WorkSubTypeListDto: WorkSubTypeDto, IPagingCounted
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string MeasurementUnitName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string MeasurementUnitValue { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ClassifierTypeName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParentCode { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParentName { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(WorkSubType))]
    public class WorkSubTypeEditDto : WorkSubTypeDto
    {
    }

    [MainEntity(nameof(WorkSubType))]
    public class WorkSubTypeDetailsDto : WorkSubTypeDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string MeasurementUnitName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string MeasurementUnitValue { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ClassifierTypeName { get; set; }

        [CaseFilter]
        public string ParentCode { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParentName { get; set; }
    }
}
