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
    [RightsCheckList(nameof(Profile), nameof(Right))]
    public class ProfileRightDto: BaseProfileRightDto
    {
    }

    [RightsCheckList(nameof(Profile), nameof(Right))]
    public class ProfileRightListDto: BaseProfileRightDto, IPagingCounted
    {
        /// <summary>
        /// Shows that both profile and right are Active (this field is present in Profile and Right)
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        [Display(Name = "Діє")]
        public virtual bool IsActive { get; set; }

        public int TotalRecordCount { get; set; }
    }

    [RightsCheckList(nameof(Profile), nameof(Right))]
    public class ProfileRightDetailDto: BaseProfileRightDto
    {
        /// <summary>
        /// Shows that both profile and right are Active (this field is present in Profile and Right)
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        [Display(Name = "Діє")]
        public virtual bool IsActive { get; set; }
    }
}
