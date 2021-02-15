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
    [MainEntity(nameof(ApplicationRowLevelRight))]
    public class ApplicationRowLevelRightDto: BaseApplicationRowLevelRightDto
    {
    }

    [MainEntity(nameof(ApplicationRowLevelRight))]
    public class ApplicationRowLevelRightListDto: BaseApplicationRowLevelRightDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ApplicationRowLevelRight))]
    public class ApplicationRowLevelRightDetailDto: BaseApplicationRowLevelRightDto
    {
    }
}
