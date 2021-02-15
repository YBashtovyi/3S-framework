using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Data.Dto.Org;

namespace App.Data.Dto.Org
{
    public class OrgEmployeeListDto: BaseOrgEmployeeDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public class OrgEmployeeDto: BaseOrgEmployeeDto
    {
    }
}
