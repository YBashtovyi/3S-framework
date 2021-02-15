using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [RightsCheckList(nameof(Profile),nameof(OperationRight))]
    public class ProfileOperationRightDto: BaseProfileOperationRightDto
    {
    }

    [RightsCheckList(nameof(Profile), nameof(OperationRight))]
    public class ProfileOperationRightDetailDto : BaseProfileOperationRightDto
    {
    }

    [RightsCheckList(nameof(Profile), nameof(OperationRight))]
    public class ProfileOperationRightListDto : BaseProfileOperationRightDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }
}
