using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Data.Dto.Org;

namespace App.Data.Dto.Org
{
    public class OrgUnitStaffListDto: BaseOrgUnitStaffDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        [CaseFilter]
        public Guid OrgUnitId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OrgUnitPositionName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string PersonFullName { get; set; }
    }

    public class OrgUnitStaffDetailsDto: BaseOrgUnitStaffDto
    {
        public string OrgUnitPositionName { get; set; }

        public string PersonFullName { get; set; }
    }

    public class OrgUnitStaffEditDto: BaseOrgUnitStaffDto
    {
        public Guid PersonId { get; set; }

        public string PersonFullName { get; set; }
    }
}
