using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Data.Dto.Org;
using Core.Data.Models.Org;

namespace App.Data.Dto.Org
{
    public class OrgUnitPositionListDto: BaseOrgUnitPositionDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        public DateTime CreatedOn { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string PositionName { get; set; }
    }

    public class OrgUnitPositionEditDto: BaseOrgUnitPositionDto
    {
    }

    public class OrgUnitPositionDetailsDto: BaseOrgUnitPositionDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string PositionName { get; set; }

        [CaseFilter]
        public string PositionCode { get; set; }
    }
}
