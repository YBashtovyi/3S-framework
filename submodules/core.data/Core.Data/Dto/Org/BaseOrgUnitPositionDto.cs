using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Org
{
    public class BaseOrgUnitPositionDto: CoreDto
    {
        [CaseFilter]
        public Guid OrgUnitId { get; set; }

        [CaseFilter]
        public Guid PositionId { get; set; }

        public int StaffUnitCount { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Description { get; set; }
    }
}
