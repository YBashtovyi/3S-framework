using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;
using Core.Security.Dto;
using Core.Security.Models;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(Profile))]
    public class ProfileDto: BaseProfileDto
    {
    }

    [MainEntity(nameof(Profile))]
    public class ProfileListDto: BaseProfileDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(Profile))]
    public class ProfileDetailDto: BaseProfileDto
    {
    }
}
