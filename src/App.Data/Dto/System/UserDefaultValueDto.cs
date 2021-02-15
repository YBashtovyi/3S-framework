using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(UserDefaultValue))]
    public class UserDefaultValueDto: BaseUserDefaultValueDto
    {
    }

    [MainEntity(nameof(UserDefaultValue))]
    public class UserDefaultValueListDto: BaseUserDefaultValueDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(UserDefaultValue))]
    public class UserDefaultValueDetailDto: BaseUserDefaultValueDto
    {
    }
}
