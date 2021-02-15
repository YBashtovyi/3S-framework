using System;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Prj
{
    [MainEntity(nameof(ProjectAdditionalAgreement))]
    public class ProjectAdditionalAgreementListDto: CoreDto, IPagingCounted
    {
        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime RegDate { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegNumber { get; set; }

        [CaseFilter]
        public string DocState { get; set; }

        public string DocStateName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal? Cost { get; set; }

        [CaseFilter]
        public Guid ParentId { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ProjectAdditionalAgreement))]
    public class ProjectAdditionalAgreementEditDto : CoreDto
    {
        public Guid ProjectId { get; set; }

        public Guid ParentId { get; set; }

        public string DocType { get; set; }
        
        public string DocState { get; set; }

        public DateTime RegDate { get; set; }

        public string RegNumber { get; set; }

        public decimal? Cost { get; set; }

        public string Description { get; set; }
    }

    [MainEntity(nameof(ProjectAdditionalAgreement))]
    public class ProjectAdditionalAgreementDetailsDto : CoreDto
    {
        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        public Guid ParentId { get; set; }

        public DateTime ParentRegDate { get; set; }

        public string ParentRegNumber { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public Guid GeneralContractorId { get; set; }

        public string GeneralContractorName { get; set; }

        public string TypeOfProjectWorkCode { get; set; }

        public string TypeOfProjectWorkName { get; set; }

        public string DocType { get; set; }

        public string DocTypeName { get; set; }

        public string DocState { get; set; }

        public string DocStateName { get; set; }

        public DateTime RegDate { get; set; }

        public string RegNumber { get; set; }

        public decimal? Cost { get; set; }

        public string Description { get; set; }
    }
}
