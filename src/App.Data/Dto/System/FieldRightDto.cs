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
    [MainEntity(nameof(FieldRight))]
    public class FieldRightDto: BaseFieldRightDto
    {
    }

    [MainEntity(nameof(FieldRight))]
    public class FieldRightListDto: BaseFieldRightDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(FieldRight))]
    public class FieldRightDetailDto: BaseFieldRightDto
    {
    }
}
