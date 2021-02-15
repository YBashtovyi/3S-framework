using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(Role))]
    public class RoleDto: BaseRoleDto
    {
    }

    [MainEntity(nameof(Role))]
    public class RoleListDto: BaseRoleDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(Role))]
    public class RoleDetailDto: BaseRoleDto
    {
    }
}
