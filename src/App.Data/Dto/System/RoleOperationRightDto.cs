using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [RightsCheckList(nameof(Role), nameof(OperationRight))]
    public class RoleOperationRightDto: BaseRoleOperationRightDto
    {
    }

    [RightsCheckList(nameof(Role), nameof(OperationRight))]
    public class RoleOperationRightDetailDto: BaseRoleOperationRightDto
    {
    }

    [RightsCheckList(nameof(Role), nameof(OperationRight))]
    public class RoleOperationRightListDto: BaseRoleOperationRightDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }
}
