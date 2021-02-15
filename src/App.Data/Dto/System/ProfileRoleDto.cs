using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [RightsCheckList(nameof(Profile), nameof(Role))]
    public class ProfileRoleDto: BaseProfileRoleDto
    {
    }

    [RightsCheckList(nameof(Profile), nameof(Role))]
    public class ProfileRoleListDto: BaseProfileRoleDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// Shows that both role and profile are Active (this field is present in Role and Profile)
        /// </summary>
        [Display(Name = "Діє")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }

    [RightsCheckList(nameof(Profile), nameof(Role))]
    public class ProfileRoleDetailDto: BaseProfileRoleDto
    {
        /// <summary>
        /// Shows that both role and profile are Active (this field is present in Role and Profile)
        /// </summary>
        [Display(Name = "Діє")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }
}
