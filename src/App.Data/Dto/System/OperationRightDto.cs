using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(OperationRight))]
    public class OperationRightDto: BaseOperationRightDto
    {
    }

    [MainEntity(nameof(OperationRight))]
    public class OperationRightDetailDto : BaseOperationRightDto
    {
    }

    [MainEntity(nameof(OperationRight))]
    public class OperationRightListDto : BaseOperationRightDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }
}
