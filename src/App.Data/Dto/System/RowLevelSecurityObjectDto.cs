using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(RowLevelSecurityObject))]
    public class RowLevelSecurityObjectDto: BaseRowLevelSecurityObjectDto
    {
    }

    [MainEntity(nameof(RowLevelSecurityObject))]
    public class RowLevelSecurityObjectListDto: BaseRowLevelSecurityObjectDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(RowLevelSecurityObject))]
    public class RowLevelSecurityObjectDetailDto: BaseRowLevelSecurityObjectDto
    {
    }
}
