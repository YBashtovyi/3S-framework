using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;
using Microsoft.Extensions.Primitives;

namespace App.Data.Dto.Prj
{
    [MainEntity(nameof(ProjectContract))]
    public class ProjectContractListDto: CoreDto, IPagingCounted
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

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal Cost { get; set; }

        [CaseFilter]
        public Guid CustomerId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string CustomerName { get; set; }

        [CaseFilter]
        public Guid GeneralContractorId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string GeneralContractorName { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ProjectContract))]
    public class ProjectContractEditDto : CoreDto
    {
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Id of the <see cref="Organization"/>
        /// PrjParticipant - project role - Customer
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Id of the <see cref="Organization"/>
        /// PrjParticipant - project role - GeneralContractor
        /// </summary>
        public Guid GeneralContractorId { get; set; }

        public string DocType { get; set; }

        public DateTime RegDate { get; set; }

        public string RegNumber { get; set; }

        public string DocState { get; set; }

        public decimal Cost { get; set; }

        public string BiddingType { get; set; }

        public string TenderCode { get; set; }

        public Guid? BiddingCode { get; set; }

        public string BiddingSubject { get; set; }

        public string Description { get; set; }
    }

    [MainEntity(nameof(ProjectContract))]
    public class ProjectContractDetailsDto : CoreDto
    {
        [CaseFilter]
        public Guid ProjectId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectName { get; set; }

        [CaseFilter]
        public Guid CustomerId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string CustomerName { get; set; }

        [CaseFilter]
        public Guid GeneralContractorId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string GeneralContractorName { get; set; }

        public string TypeOfProjectWorkCode { get; set; }

        public string TypeOfProjectWorkName { get; set; }

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

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal Cost { get; set; }

        public string BiddingType { get; set; }

        public string BiddingTypeName { get; set; }

        public string TenderCode { get; set; }

        public Guid? BiddingCode { get; set; }

        public string BiddingSubject { get; set; }

        public string Description { get; set; }
    }

    [MainEntity(nameof(ProjectContract))]
    public class ProjectContractAddAgreementListDto : CoreDto, IPagingCounted
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

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal? Cost { get; set; }

        [CaseFilter]
        public Guid CustomerId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string CustomerName { get; set; }

        [CaseFilter]
        public Guid GeneralContractorId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string GeneralContractorName { get; set; }

        public Guid? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
