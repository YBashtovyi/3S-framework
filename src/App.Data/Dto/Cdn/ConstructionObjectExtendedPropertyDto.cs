using System;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Cdn
{
    [MainEntity(nameof(ConstructionObjectExtendedProperty))]
    public class ConstructionObjectExtendedPropertyDto: CoreDto
    {
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Id of <see cref="ConstructionObject"/>
        /// </summary>
        [CaseFilter]
        public Guid ConstructionObjectId { get; set; }

        /// <summary>
        /// Id of <see cref="ConstructionObjectExPropertyDictionary"/>
        /// </summary>
        [CaseFilter]
        public Guid DictionaryId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Value { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ValueJson { get; set; }
    }

    [MainEntity(nameof(ConstructionObjectExtendedProperty))]
    public class ConstructionObjectExtendedPropertyListDto: CoreDto, IPagingCounted
    {
        /// <summary>
        /// Id of <see cref="ConstructionObject"/>
        /// </summary>
        [CaseFilter]
        public Guid ConstructionObjectId { get; set; }

        /// <summary>
        /// Id of <see cref="ConstructionObjectExPropertyDictionary"/>
        /// </summary>
        [CaseFilter]
        public Guid ConstructionObjectExPropertyId { get; set; }
        [CaseFilter]
        public string ConstructionObjectExPropertyCode { get; set; }
        public string ConstructionObjectExPropertyName { get; set; }

        /// <summary>
        /// Id of <see cref="ConstructionObjectExPropertyDictionary"/>
        /// </summary>
        [CaseFilter]
        public Guid ConstructionObjectSubExPropertyId { get; set; }
        [CaseFilter]
        public string ConstructionObjectSubExPropertyCode { get; set; }
        public string ConstructionObjectSubExPropertyName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
