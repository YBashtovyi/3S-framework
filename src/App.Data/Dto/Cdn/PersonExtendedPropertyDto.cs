using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Cdn
{
    [MainEntity(nameof(Models.PersonExtendedProperty))]
    public class PersonExtendedPropertyListDto: CoreDto, IPagingCounted
    {
        [CaseFilter]
        public virtual Guid PersonId { get; set; }

        [CaseFilter]
        public virtual string PersonExtendedProperty { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string PersonExtendedPropertyName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Value { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string ValueJson { get; set; }

        [CaseFilter(CaseFilterOperation.ValueRange)]
        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
