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
    [RightsCheckList(nameof(Role), nameof(Right))]
    public class RoleRightDto: BaseRoleRightDto
    {
    }

    [RightsCheckList(nameof(Role), nameof(Right))]
    public class RoleRightListDto: BaseRoleRightDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// Shows that role and right are Active (this field is present in Role and Right)
        /// </summary>
        [Display(Name = "Діє")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }

    [RightsCheckList(nameof(Role), nameof(Right))]
    public class RoleRightDetailDto: BaseRoleRightDto
    {
        /// <summary>
        /// Shows that role and right are Active (this field is present in Role and Right)
        /// </summary>
        [Display(Name = "Діє")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }
}
