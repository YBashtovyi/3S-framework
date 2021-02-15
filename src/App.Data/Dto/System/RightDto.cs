using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(Right))]
    public class RightDto: BaseRightDto
    {
    }

    [MainEntity(nameof(Right))]
    public class RightListDto: BaseRightDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(Right))]
    public class RightDetailDto: BaseRightDto
    {
    }
}
