using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Dto.Common.NotMapped;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Prj
{
    [MainEntity(nameof(ProjectPhotoReport))]
    public class ProjectPhotoReportListDto: CoreDto, IPagingCounted
    {
        [CaseFilter]
        public string DocState { get; set; }

        public string DocStateName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime RegDate { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegNumber { get; set; }

        [CaseFilter]
        public string DocType { get; set; }

        public string DocTypeName { get; set; }

        [CaseFilter]
        public Guid ProjectId { get; set; }

        [CaseFilter]
        public Guid CustomerId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string CustomerName { get; set; }

        [CaseFilter]
        public Guid GeneralContractorId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string GeneralContractorName { get; set; }

        [CaseFilter]
        public string FixationType { get; set; }

        public string FixationTypeName { get; set; }

        [CaseFilter]
        public string FixationState { get; set; }

        public string FixationStateName { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ProjectPhotoReport))]
    public class ProjectPhotoReportEditDto : CoreDto
    {
        [CaseFilter]
        public string DocState { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime RegDate { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegNumber { get; set; }

        [CaseFilter]
        public string DocType { get; set; }

        [CaseFilter]
        public Guid ProjectId { get; set; }

        [CaseFilter]
        public Guid CustomerId { get; set; }

        [CaseFilter]
        public Guid GeneralContractorId { get; set; }

        [CaseFilter]
        public string FixationType { get; set; }

        [CaseFilter]
        public string FixationState { get; set; }

        public Guid? ResponsibleEmployeeId { get; set; }

        public string Description { get; set; }
    }

    [MainEntity(nameof(ProjectPhotoReport))]
    public class ProjectPhotoReportDetailsDto : CoreDto
    {
        [CaseFilter]
        public string DocState { get; set; }

        public string DocStateName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime RegDate { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegNumber { get; set; }

        [CaseFilter]
        public string DocType { get; set; }

        public string DocTypeName { get; set; }

        [CaseFilter]
        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        [CaseFilter]
        public Guid CustomerId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string CustomerName { get; set; }

        [CaseFilter]
        public Guid GeneralContractorId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string GeneralContractorName { get; set; }

        [CaseFilter]
        public string FixationType { get; set; }

        public string FixationTypeName { get; set; }

        [CaseFilter]
        public string FixationState { get; set; }

        public string FixationStateName { get; set; }

        public Guid? ResponsibleEmployeeId { get; set; }

        public string ResponsibleEmployeeFullName { get; set; }

        public string Description { get; set; }

        public string TypeOfProjectWorkCode { get; set; }

        public string TypeOfProjectWorkName { get; set; }

        public string AtuCoordinates { get; set; }

        [NotMapped]
        public MapCoordinate AtuCoordinateList { get; set; } = new MapCoordinate();
    }
}
