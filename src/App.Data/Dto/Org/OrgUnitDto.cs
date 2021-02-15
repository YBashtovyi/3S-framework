using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Dto.Org
{
    [MainEntity(nameof(OrgUnit))]
    public class OrgUnitListDto: CoreDto, IPagingCounted
    {
        public DateTime CreatedOn { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string DerivedEntity { get; set; }

        public int TotalRecordCount { get; set; }
    }
}
