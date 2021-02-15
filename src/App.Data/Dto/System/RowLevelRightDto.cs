using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(RowLevelRight))]
    public class RowLevelRightDto: BaseRowLevelRightDto
    {
    }

    [MainEntity(nameof(RowLevelRight))]
    public class RowLevelRightListDto: BaseRowLevelRightDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(RowLevelRight))]
    public class RowLevelRightDetailDto: BaseRowLevelRightDto
    {
    }
}
