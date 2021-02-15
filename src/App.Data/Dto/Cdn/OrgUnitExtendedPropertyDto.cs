using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Cdn
{
    [MainEntity(nameof(OrgUnitExtendedProperty))]
    public class OrgUnitExtendedPropertyListDto: CoreDto, IPagingCounted
    {
        [CaseFilter]
        public Guid OrgUnitId { get; set; }

        [CaseFilter]
        public string OrgExtendedProperty { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string OrgExtendedPropertyName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Value { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ValueJson { get; set; }

        [CaseFilter(CaseFilterOperation.ValueRange)]
        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
