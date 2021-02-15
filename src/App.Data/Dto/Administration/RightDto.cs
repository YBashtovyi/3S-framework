using System;
using System.Collections.Generic;
using System.Text;
using Core.Administration.Models;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Dto.Administration
{
    [MainEntity(nameof(Right))]
    public class RightListDto: CoreDto, IPagingCounted
    {
        public DateTime CreatedOn { get; set; }

        public string RightType { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(Right))]
    public class RightDetailsDto : CoreDto
    {
        public string RightType { get; set; }

        public string RightTypeName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }
    }

    [MainEntity(nameof(Right))]
    public class RightEditDto : CoreDto
    {
        public string RightType { get; set; }

        public string RightTypeName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }
    }
}
