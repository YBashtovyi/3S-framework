using System;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    
    [MainEntity(nameof(UserProfile))]
    [RlsRight(nameof(Department), nameof(DepartmentId))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    [RlsRight(nameof(Employee), nameof(UserId))]
    public class UserProfileDto: BaseUserProfileDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid OrganizationId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid DepartmentId { get; set; }
    }

    [MainEntity(nameof(UserProfile))]
    [RlsRight(nameof(Department), nameof(DepartmentId))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    [RlsRight(nameof(Employee), nameof(UserId))]
    public class UserProfileListDto: BaseUserProfileDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid OrganizationId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid DepartmentId { get; set; }
    }

    [MainEntity(nameof(UserProfile))]
    [RlsRight(nameof(Department), nameof(DepartmentId))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    [RlsRight(nameof(Employee), nameof(UserId))]
    public class UserProfileDetailDto: BaseUserProfileDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid OrganizationId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid DepartmentId { get; set; }
    }
}
