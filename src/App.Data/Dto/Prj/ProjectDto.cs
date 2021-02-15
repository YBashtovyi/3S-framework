using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Dto.Common.NotMapped;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Prj
{
    [MainEntity(nameof(Project))]
    [RlsRight(nameof(OrgUnit), nameof(OwnerId))]
    public class ProjectListDto: CoreDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        [CaseFilter]
        public Guid OwnerId { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime CreatedOn { get; set; }

        [CaseFilter]
        public string Code { get; set; }

        /// <summary>
        /// Code from <see cref="ConstructionObject"/>
        /// </summary>
        [CaseFilter]
        public string ConstructionObjectCode { get; set; }

        [CaseFilter]
        public string ProjectStatus { get; set; }

        [CaseFilter]
        public Guid RegionId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal Cost { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime DateBegin { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime DateEnd { get; set; }

        [CaseFilter]
        public string ProjectImplementationState { get; set; }

        [CaseFilter]
        public string TypeOfFinancing { get; set; }

        /// <summary>
        /// Id from <see cref="TypeOfProjectWork"/>
        /// </summary>
        [CaseFilter]
        public Guid TypeOfProjectWorkId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string AtuCoordinates { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegionName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectStatusName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectImplementationStateName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParticipantOrgName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string GeneralContractorName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string CustomerName { get; set; }
    }

    [MainEntity(nameof(Project))]
    [RlsRight(nameof(OrgUnit), nameof(OwnerId))]
    public class ProjectEditDto: CoreDto
    {
        public Guid OwnerId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid RegionId { get; set; }

        public Guid? DistrictId { get; set; }

        public string ProjectStatus { get; set; }

        public string TypeOfFinancing { get; set; }

        public decimal Cost { get; set; }

        public string ProjectImplementationState { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string FullName { get; set; }

        public Guid ConstructionObjectId { get; set; }

        public Guid TypeOfProjectWorkId { get; set; }

        public string RepairLength { get; set; }

        public string RepairSquare { get; set; }

        public string Description { get; set; }
    }

    [MainEntity(nameof(Project))]
    [RlsRight(nameof(OrgUnit), nameof(OwnerId))]
    public class ProjectDetailsDto: CoreDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public Guid RegionId { get; set; }

        public Guid? DistrictId { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public decimal Cost { get; set; }

        public string ProjectStatus { get; set; }

        public string ProjectImplementationState { get; set; }

        public string TypeOfFinancing { get; set; }

        public string FullName { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedBy { get; set; }

        public string CreatedFullName { get; set; }

        public string OwnerName { get; set; }

        public string RegionName { get; set; }

        public string DistrictName { get; set; }

        public string ProjectStatusName { get; set; }

        public string ProjectImplementationStateName { get; set; }

        public string TypeOfFinancingName { get; set; }
        
        public Guid ConstructionObjectId { get; set; }

        public string ConstructionObjectName { get; set; }

        public string TypeOfProjectWorkName { get; set; }

        public string TypeOfProjectWorkCode { get; set; }

        public string RepairLength { get; set; }

        public string RepairSquare { get; set; }

        public string Description { get; set; }

        public string AtuCoordinates { get; set; }

        [NotMapped]
        public MapCoordinate AtuCoordinateList { get; set; } = new MapCoordinate();
    }

    [MainEntity(nameof(Project))]
    [RlsRight(nameof(OrgUnit), nameof(OwnerId))]
    public class ProjectListExcelDto : CoreDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        [CaseFilter]
        public Guid OwnerId { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime CreatedOn { get; set; }

        [CaseFilter]
        public string Code { get; set; }

        /// <summary>
        /// Code from <see cref="ConstructionObject"/>
        /// </summary>
        [CaseFilter]
        public string ConstructionObjectCode { get; set; }

        [CaseFilter]
        public string ProjectStatus { get; set; }

        [CaseFilter]
        public Guid RegionId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public decimal Cost { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime DateBegin { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime DateEnd { get; set; }

        [CaseFilter]
        public string ProjectImplementationState { get; set; }

        [CaseFilter]
        public string TypeOfFinancing { get; set; }

        /// <summary>
        /// Id from <see cref="TypeOfProjectWork"/>
        /// </summary>
        [CaseFilter]
        public Guid TypeOfProjectWorkId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string AtuCoordinates { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string RegionName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectStatusName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectImplementationStateName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParticipantOrgName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string GeneralContractorName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string CustomerName { get; set; }

        [Display(Name = "Район")]
        public string DistrictName { get; set; }

        [Display(Name = "Протяжність ділянки ремонту, км")]
        public string RepairLength { get; set; }

        [Display(Name = "Площа ділянки ремонту, м2")]
        public string RepairSquare { get; set; }
    }

    [MainEntity(nameof(Project))]
    public class ProjectParticipantEmployeeListDto: CoreDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string OrganizationName { get; set; }

        [CaseFilter]
        public Guid EmployeeId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string EmployeeFullName { get; set; }
    }
}
